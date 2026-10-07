# L02-05 - Prijsopzoeking

*Price Check on Aisle Two Million*

## Symptoom
Twee miljoen prijsopzoekingen tegen een cache van 300 items duren ongeveer **50 ms** en alloceren **153 MB**. Bijna elke call is een cache hit
die onmiddellijk terugkeert zonder ergens op te wachten. Toch worden er ongeveer 80 bytes per call gealloceerd.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 30 ref-ms |
| Mediaan gealloceerd | 0,5 MB |

## Noot
Tijd wordt geschaald naar jouw machine; allocatie- en collectieaantallen niet.
