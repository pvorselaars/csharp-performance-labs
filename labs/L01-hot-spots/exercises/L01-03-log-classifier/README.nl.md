# L01-03 - Logclassificatie

*Lees de logs, zeiden ze*

## Symptoom
Het parsen van 40.000 logregels duurt ongeveer **0,25 seconden** en alloceert zo'n **380 MB**. De parse-logica
is een paar regels per logregel. Dat zou niet zoveel moeten kosten. De GC is druk bezig (gen0- en
gen1-aantallen zijn niet-nul in de harness-output).

## Doel
Dezelfde geparste resultaten (checksum), en:

| Budget | Waarde      |
|---|------------|
| Mediane tijd | 147 ref-ms |
| Mediaan aantal allocaties | 100 MB     |
