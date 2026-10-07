# L02-01 - Sensormetingen

*Sensor and Sensibility*

## Symptoom
Het samenvatten van 400.000 sensormetingen (som, max, mediaan en een histogram) duurt ongeveer **90 ms** en alloceert ongeveer
**45 MB**, terwijl de input een `int[]` is die al bestaat en het resultaat vijf getallen zijn. De harness laat zien dat de GC
niet eens druk is (0 tot 1 collecties), toch is het aantal gealloceerde bytes veel groter dan het antwoord nodig heeft.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 25 ref-ms |
| Mediaan gealloceerd | 5 MB |

## Noot over de inputdata
De *input* van de workload wordt één keer gegenereerd, bij eerste gebruik. De harness warmt op voordat hij meet, dus de input
telt niet mee als allocatie. Alleen het algoritme dat je aan het fixen bent telt wel. (Tijd wordt geschaald naar jouw machine; allocatie niet.)
