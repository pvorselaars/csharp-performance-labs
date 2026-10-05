# L03-05 - Oplossing

## Wat het profiel laat zien
- **snapshots:** +20.000 `Document`, `byte[]` (4.096), `Metadata`, `string`. Retentiepad: static `Dictionary<object, Metadata>` in `Tags` → entries → `Document`.

## Grondoorzaak
Het gebruiken van een gewone `Dictionary` om data aan objecten te hangen, maakt de dictionary een eigenaar van die objecten: hij houdt elke key sterk vast en heeft geen idee wanneer de key verder klaar is.

## Fix
Gebruik `ConditionalWeakTable<object, Metadata>`. Deze houdt keys zwak vast, en de value leeft precies zo lang als de key. Regel: de value mag niet buiten de controle van de table om sterk naar zijn eigen key verwijzen, anders bouw je het lek opnieuw op (de table kan prima omgaan met een value die naar zijn key verwijst, maar een *static* reference van elders zou dat niet kunnen).

## Take-aways
1. **Een dictionary met objecten als key die je niet zelf bezit, is een lek in wording**; vraag jezelf af: 'wie verwijdert de entry?'.
2. `ConditionalWeakTable` koppelt de levensduur van de entry aan die van de key; `WeakReference<T>` in een gewone collection heeft nog steeds iemand nodig die dode entries verwijdert.
3. Dit is het mechanisme achter 'attached properties' en veel caches die op objecten gesleuteld zijn. Het vergelijkt keys altijd op *reference*.
4. `Reset()` is hier alleen zodat runs zich niet opstapelen; productiecode heeft zo'n knop niet.

## Extra credit
Wat gebeurt er als `Metadata` een sterke reference terug naar zijn `Document` houdt in de `ConditionalWeakTable`-versie? Probeer het en kijk naar het kept-cijfer (hier bestaat een gedocumenteerde garantie).

## Go further
Vervang de table door een `Dictionary<int, WeakReference<Document>>` en voeg cleanup toe. Tel hoeveel code je daarvoor nodig had, vergeleken met `ConditionalWeakTable`.
