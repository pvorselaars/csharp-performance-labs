# L06-08 - Count matches

*Counting Sheep*

## Symptoom
Een monitor scant een buffer van 256.000 metingen 400 keer opnieuw, en telt hoeveel er gelijk zijn aan 42. Dat zijn 100 miljoen vergelijkingen, en het duurt **~31 ms**: ongeveer 0,3 ns per element. De code is één leesbare LINQ-expressie. De buffer is 1 MB, dus hij past in de CPU-cache en geheugensnelheid is niet de beperkende factor.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 8 ref-ms |
| Mediaan gealloceerd | 1 MB |
