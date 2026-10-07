# L02-08 - Kleine sommen

*Small Change*

## Symptoom
Het optellen van een lijst van 24 elementen, twee miljoen keer, alloceert **~50 MB** en is trager dan de rekensom doet vermoeden. Het resultaat is één getal per call, toch produceert elke call rommel.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 15 ref-ms |
| Mediaan gealloceerd | 0 MB |
