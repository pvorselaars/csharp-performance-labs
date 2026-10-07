# L02-04 - Tile-cache

*Tiles All the Way Down*

## Symptoom
Het renderen van 1,5 miljoen map-tiles duurt ongeveer **400 ms** en alloceert **172 MB**, wat redelijk is voor 1,5M kleine objecten met elk
een buffer van 64 bytes. Maar de harness laat een niet-nul **gen1**-aantal zien voor objecten die maar één iteratie leven, en de tijd is
veel meer dan 1,5M kleine allocaties zouden moeten kosten. Het allocatiebudget is hier bewust ruim: **het probleem is niet hoeveel bytes je alloceert.**

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 100 ref-ms |
| Mediaan gealloceerd | 200 MB |

## Noot
Tijd wordt geschaald naar jouw machine; allocatie- en collectieaantallen niet.