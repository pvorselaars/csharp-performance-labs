# L06-01 - Matrix walk

*Grid Lock*

## Symptoom
Het optellen van een 4096×4096-grid aan integers duurt **~100 ms**. De loop doet precies N² optellingen en alloceert niets. Niets te optimaliseren dus?

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 10 ref-ms |
| Mediaan gealloceerd | 1 MB |
