# L05-03 - SKU-totalen

*The Long Tally*

## Symptoom
Het optellen van de verkopen voor 100 SKU's uit een tabel van 200.000 rijen duurt **~0,5 s**. Elke query is piepklein en de C# is één regel. Er wordt niet veel toegewezen en er oogt niets mis in de applicatiecode. De tijd gaat volledig op *binnen de database*.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 25 ref-ms |
| Mediaan toegewezen | 2 MB |
