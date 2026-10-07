# L04-03 - Bulk fetch

*Release the Kraken*

## Symptoom
Het ophalen van 1.000 items van een downstream-service duurt **zo'n halve seconde**, en de downstream krijgt daarbij ~1.000 requests tegelijk in flight. De downstream beantwoordt een enkele request snel, en de code is geschreven om zo snel mogelijk te zijn.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 150 ref-ms |
| Mediaan toegewezen | 1 MB |
| peakInflight | ≤ 50 |

## Opmerking
De "downstream-service" in deze exercise wordt **trager naarmate er meer requests tegelijk in flight zijn** (zoals de meeste echte services ook doen). Dat is precies het punt.
