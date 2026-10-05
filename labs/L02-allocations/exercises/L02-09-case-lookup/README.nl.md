# L02-09 - Case-opzoeking

*Case Closed*

## Symptoom
Een hoofdletterongevoelige dictionary-opzoeking die niets zou moeten alloceren, doet dat toch niet: 400.000 opzoekingen alloceren **~20 MB** aan korte strings en besteden een groot deel van de tijd aan het aanmaken daarvan.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 43 ref-ms |
| Mediaan gealloceerd | 1 MB |
