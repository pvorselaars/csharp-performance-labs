# L01-04 - Hoeveelheden parsen

*Garbage In, Time Out*

## Symptoom
Het opschonen van één spreadsheetkolom van 1.000.000 cellen duurt bijna **een seconde**. Ruwweg de helft van
de cellen is rommel (`N/A`, leeg, `--`, ...) en wordt simpelweg overgeslagen. De code heeft geen geneste
loops en geen I/O, en is "overduidelijk O(n)". Toch zou 1.000.000 cellen niet zo lang moeten duren.

## Doel
Dezelfde geparste waarden (checksum), en:

| Budget | Waarde     |
|---|-----------|
| Mediane tijd | 150 ref-ms |
| Mediaan aantal allocaties | 45 MB     |

## Extra experiment
Draai de trage versie één keer onder een **debugger** (geen profiler, gewoon normaal doorstappen) en vergelijk.
De harness waarschuwt je wanneer een debugger is gekoppeld. Waarom maakt dat zoveel uit voor juist dit probleem?
