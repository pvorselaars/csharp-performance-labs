# L05-07 - Audit-log

*Paper Trail*

## Symptoom
Het wegschrijven van 20.000 korte audit-regels duurt **~45 ms** op deze machine (en langer op een echte schijf of netwerkschijf), hoewel de totale data maar zo'n 500 KB is. De CPU is niet druk bezig. Het grootste deel van de tijd zit in het besturingssysteem.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 2 ref-ms |
| Mediaan toegewezen | 2 MB |
