# L06-03 - Threshold count

*Over the Threshold*

## Symptoom
Het tellen van metingen boven een drempel over 1 miljoen willekeurige bytes, 60 passes, duurt **~200–300 ms**: enkele nanoseconden per element voor een vergelijk-en-tel. De loop-body is zo simpel als code maar kan zijn.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 60 ref-ms |
| Mediaan gealloceerd | 1 MB |
