# L02-06 - Logclassificatie, deel 2

*Log Rolling*

## Symptoom
Dit is waar **L01-03 stopte**: de regex wordt één keer gebouwd en is compiled, dus is dat niet langer het *tijd*probleem. Het parsen van 100.000 logregels
duurt nog steeds ongeveer **50 ms** en alloceert ongeveer **89 MB**, wat ruwweg 900 bytes rommel per regel is, voor een resultaat dat één klein object is.
(De 100.000 inputstrings worden één keer gegenereerd, vóór de meting, en tellen niet mee.)

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 10 ref-ms |
| Mediaan gealloceerd | 12 MB |

## Noot over de inputdata
De *input* van de workload wordt één keer gegenereerd, bij eerste gebruik. De harness warmt op voordat hij meet, dus de input
telt niet mee als allocatie. Alleen het algoritme dat je aan het fixen bent telt wel. (Tijd wordt geschaald naar jouw machine; allocatie niet.)