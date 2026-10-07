# L00-01 - Oplossing

## Wat het profiel laat zien
> Het frame met de meeste eigen tijd, `System.Buffer.BulkMoveWithWriteBarrierBatch`, is waargenomen in een Rider sampling-profiel (Release-build, profile-modus); de rest van de vorm van de call tree volgt uit wat de code impliceert.

- **Sampling:** vrijwel alle eigen tijd zit in één runtime-routine, `System.Buffer.BulkMoveWithWriteBarrierBatch(ref Byte, ref Byte, UIntPtr)`, bereikt via `Array.Copy` in `List<T>.Insert`, vanuit `Feed.Build`. Het is geen gewone `memmove`: zie de root cause.
  De constructie van `FeedItem` en de checksum-lus zijn ruis.
- **Allocatie:** ≈ 2 MB, de 60.000 `FeedItem`-objecten. De GC staat stil, dus allocatie is niet het probleem.

## Root cause
`List<T>.Insert(0, x)` schuift elk bestaand element één plek naar rechts. De k-de insert verschuift k elementen, dus 60.000 inserts verschuiven in totaal ongeveer 1,8 miljard elementslots: O(n²) werk in totaal.
Het aantal verdubbelen maakt de tijd ruwweg vier keer zo lang.

**Waarom het hot frame geen gewone `memmove` is.** `FeedItem` is een `record`, dus een referentietype, waardoor de array van de list GC-referenties bevat. Voor een array waarvan het elementtype GC-referenties bevat, gebruikt `Array.Copy` geen `Memmove`: het roept `Buffer.BulkMoveWithWriteBarrier` aan zodat de garbage collector op de hoogte wordt gesteld van de verplaatste referenties [[108]](../../../../docs/READING-LIST.md#ref108), [[109]](../../../../docs/READING-LIST.md#ref109). Blokken groter dan 16 KB gaan via `BulkMoveWithWriteBarrierBatch`, dat in stukken van 16 KB kopieert en de GC tussen die stukken de kans geeft om te draaien. Elke volledige `Insert` hier verplaatst ongeveer 480 KB (60.000 referenties × 8 bytes), dus elke aanroep volgt dat pad.

## Fix
Voeg toe (`Add`, O(1) geamortiseerd) in omgekeerde volgorde: O(n) in totaal. Geef de list ook vooraf een capaciteit mee (`new List<FeedItem>(count)`).

## Lessen
1. **Totale kosten = kosten per aanroep × aantal aanroepen.** Elke `Insert` kost microseconden en lijkt onschuldig; het *aantal* en de groeiende omvang maken het kwadratisch.
2. Een profiel dat vrijwel volledig uit één data-verplaatsingsroutine bestaat (hier `BulkMoveWithWriteBarrierBatch`) en geen allocatie laat zien, vertelt je de categorie (CPU, dataverplaatsing) nog voordat je code hebt gelezen. De *naam* van de routine vertelt je meer: hij verplaatst referenties, wat een aanwijzing is over het elementtype.
3. Voorspellingscheck (zie het uitgewerkte LAB-LOG): 60.000 → 600.000 items zou ongeveer 100× moeten kosten (10² voor n²), niet 10×.

## Breek je eigen fix
De fix klopt niet als lezers de feed **tussen** inserts door moeten kunnen lezen (je kunt niet "aan het einde" omkeren als iemand na elke insert leest). Gebruik dan een structuur met O(1) front-insertie:
een ring buffer / `LinkedList<T>` (slechte cache locality, dus meet het), of houd de list in aankomstvolgorde en *indexeer van achteren* bij het lezen.
Bij een aantal van 5 is het origineel prima: optimaliseer nooit iets wat het profiel niet laat zien.

## Extra credit
Verander het aantal naar 600.000 (in een scratch-kopie; de checksum komt dan niet meer overeen, dus meet gewoon de tijd). Voorspel eerst: 10x, 100x, of meer? Meet daarna. Wat zegt het antwoord over het algoritme, en over de cache?
