# L06-09 - Bit counts

*Bit by Bit*

## Symptoom
Het tellen van de gezette bits in 4 miljoen 64-bit woorden duurt **~60 ms**. De loop gebruikt de bekende 'wis het laagste gezette bit'-truc, die al slimmer is dan elk bit afzonderlijk testen. Toch is het traag.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 5 ref-ms |
| Mediaan gealloceerd | 0 MB |
