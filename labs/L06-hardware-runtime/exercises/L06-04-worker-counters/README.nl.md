# L06-04 - Worker counters

*Eight Is Not Enough*

## Symptoom
Acht threads verhogen elk **hun eigen** teller 5 miljoen keer. Er is geen sharing (elke thread raakt alleen zijn eigen element aan) en geen lock, toch duurt de run **honderden milliseconden**, en acht threads zijn niet sneller dan één (of zelfs trager). Niets in de code is gedeeld of bevochten.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 120 ref-ms |
| Mediaan gealloceerd | 1 MB |

Vereist **minstens 8 cores** om het effect duidelijk te laten zien (het resultaat hangt ook af van op welke cores de threads terechtkomen).
