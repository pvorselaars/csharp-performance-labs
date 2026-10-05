# L03-04 - Oplossing

## Wat het profiel laat zien
- **Harness:** gen2 ≈ 150 per run (één per batch) in de trage versie; kept ≈ 0 MB in beide. Een lek zou een groeiende kept-waarde laten zien.
- **timeline:** blokkerende GC-pauzes na elke batch.

## Grondoorzaak
Er is geen lek. Elke batch maakt tijdelijke arrays aan die meteen garbage worden. De 'groei' was niet-verzamelde garbage: de GC draait wanneer de allocatiedruk daarom vraagt. De `GC.Collect()`-'fix' forceert 150 keer per run een blokkerende gen2-collectie, wat pure overhead is.

## Fix
Verwijder de geforceerde collectie. Als het geheugen echt lager moet blijven, fix dan het *allocatiepatroon* (pool de grote arrays; zie L02-03) of configureer de GC (bijv. `GCHeapHardLimit`, geheugenbesparende instellingen), in plaats van `GC.Collect()` aan te roepen.

## Take-aways
1. **Groeien is niet lekken.** Een lek is geheugen dat *bereikbaar* blijft; controleer de behouden grootte na een geforceerde volledige GC, of diff twee snapshots.
2. `GC.Collect()` is bijna nooit de juiste fix in productie: het maakt van een goedkoop, goed afgesteld achtergrondproces een blokkerend, volledig proces.
3. Een geheugengrafiek lezen: een zaagtand die terugkeert naar dezelfde basislijn is gezond; een basislijn die alleen maar oploopt is een lek.
4. De kept-na-GC-gate in deze harness onderscheidt de twee gevallen direct.

## Extra credit
Verander één batch zodat hij *daadwerkelijk* lekt (voeg de `big`-array toe aan een static lijst). Wat doet het kept-cijfer dan? Nu heb je een lek-fingerprint om mee te vergelijken.

## Go further
Huur de arrays van `ArrayPool` (L02-03) en vergelijk gen2-aantallen en piek working set. Zet daarna `GCHeapHardLimit` via `DOTNET_GCHeapHardLimit` en observeer.
