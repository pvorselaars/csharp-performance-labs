# L05-08 - Byte-reader

*Byte-Sized*

## Symptoom
Het optellen van de bytes van een bestand van 400 KB duurt **~150 ms**, grotendeels buiten jouw code, en de CPU is druk bezig in de kernel. Het lezen van 400.000 bytes zou een fractie van een milliseconde moeten kosten vanuit het geheugen.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 5 ref-ms |
| Mediaan toegewezen | 1 MB |
