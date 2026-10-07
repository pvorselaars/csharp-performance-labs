# L02-02 - Orderregels

*Line by Line*

## Symptoom
Het parsen van 200.000 orderregels in de vorm `10423; eu ;17;19.99;Gift|FRAGILE` duurt ongeveer **50 ms** en alloceert ongeveer **106 MB**:
ruwweg 530 bytes rommel per regel, voor een resultaat dat één klein struct is. De GC is druk (gen0-collecties bij elke run).

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 30 ref-ms |
| Mediaan gealloceerd | 1 MB |

## Noot over de inputdata
De *input* van de workload wordt één keer gegenereerd, bij eerste gebruik. De harness warmt op voordat hij meet, dus de input
telt niet mee als allocatie. Alleen het algoritme dat je aan het fixen bent telt wel. (Tijd wordt geschaald naar jouw machine; allocatie niet.)
