# L06-05 - Transform batch

*Heavy Lifting*

## Symptoom
Het 20 miljoen keer aanroepen van een kleine transform-methode kost **~5 ns per aanroep** voor iets dat drie vermenigvuldigingen is. De aanroep ziet eruit als een gewone instance-call, dus het is niet vanzelfsprekend waar een per-aanroep-kost vandaan zou kunnen komen.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 40 ref-ms |
| Mediaan gealloceerd | 0 MB |
