# L01-06 - Oplossing

## Wat het profiel laat zien
- **Sampling:** `ArraySortHelper`-frames domineren; **tracing:** 6.000 `Sort`-aanroepen over lijsten die groeien tot 6.000.

## Grondoorzaak
De hele lijst opnieuw sorteren bij elke insert: het totale werk groeit kwadratisch, en dat om een vraag te
beantwoorden (`wat is het maximum?`) die O(1) nodig heeft.

## Fix
Houd het maximum incrementeel bij. Heb je echt geordende toegang nodig, gebruik dan een structuur die
volgorde bewaart (`SortedSet<T>`, een heap/`PriorityQueue`) of sorteer één keer aan het eind.

Tijden hangen af van je machine; verhoudingen en de allocatie-/GC-getallen zouden er vergelijkbaar uit moeten zien.

## Take-aways
1. **Onderhoud de invariant die je nodig hebt, niet een sterkere.** Gesorteerd is sterker dan 'maximum'.
2. Werk binnen een loop vermenigvuldigt: kosten per aanroep x aanroepen x groei.
3. Vraag jezelf af 'wat leest de aanroeper eigenlijk?' voordat je een structuur kiest.

## Extra credit
Verdubbel het aantal naar 12.000. Voorspel de verhouding voor de trage versie, en meet daarna.

## Verder
Ondersteun 'top 10 na elke insert' met een `PriorityQueue` of een begrensde gesorteerde lijst. Vergelijk met opnieuw sorteren.
