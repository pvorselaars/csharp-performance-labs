# L02-07 - Routestatistieken

*Counting Is Hard*

## Symptoom
Het tellen van één miljoen requests per (tenant, route, status) duurt ongeveer **91 ms** en alloceert ongeveer **79 MB**, maar de resulterende tabel heeft
maar zo'n 48.000 unieke entries. Vrijwel alle allocatie is niet de tabel, maar iets dat per *request* wordt opgebouwd en weer weggegooid.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 20 ref-ms |
| Mediaan gealloceerd | 2 MB |

## Noot over de inputdata
De *input* van de workload wordt één keer gegenereerd, bij eerste gebruik. De harness warmt op voordat hij meet, dus de input
telt niet mee als allocatie. Alleen het algoritme dat je aan het fixen bent telt wel. (Tijd wordt geschaald naar jouw machine; allocatie niet.)