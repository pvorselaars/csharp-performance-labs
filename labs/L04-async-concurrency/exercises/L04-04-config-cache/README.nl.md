# L04-04 - Config cache

*Same Question, Same Moment*

## Symptoom
Acht threads vragen op hetzelfde moment dezelfde sectie op bij een cache. De kloktijd ziet er prima uit (de threads draaien parallel), maar de totale **CPU-tijd ligt een flink stuk hoger dan zou moeten**, en de loader wordt 100+ keer aangeroepen voor 20 verschillende secties. De harness rapporteert `factoryCalls` en de CPU-tijd van het proces.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 120 ref-ms |
| Mediaan toegewezen | 1 MB |
| Mediane CPU-tijd | 120 ref-ms |
| factoryCalls | ≤ 20 |

## Opmerking
Vereist **minimaal 4 cores** om het effect te laten zien. De exercise start zijn eigen threads/tasks, dus het resultaat hangt niet af van hoe druk jouw machine is, maar een 2-core machine zal de verbetering onderrapporteren.
