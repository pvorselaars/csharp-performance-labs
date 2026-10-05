# L04-07 - Partner API calls

*Politeness Has a Price*

## Symptoom
200 onafhankelijke calls naar een API die elk in 10 ms antwoordt, duren samen **zo'n 2 seconden**: precies 200 x 10 ms. De eigenaren van de API zeggen dat die tientallen gelijktijdige calls aankan zonder trager te worden. Niets staat onder druk: de CPU is idle, de threads zijn idle.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 100 ref-ms |
| Mediaan toegewezen | 1 MB |
