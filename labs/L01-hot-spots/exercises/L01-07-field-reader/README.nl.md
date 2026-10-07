# L01-07 - Veldlezer

*Kies een kolom, willekeurig welke*

## Symptoom
Een configureerbaar rapport telt een kolom op die **op naam** gekozen wordt, over 200.000 items, drie keer.
Dit duurt **~125 ms en alloceert tientallen MB's**, voor iets wat een paar miljoen optellingen zou moeten zijn.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 25 ref-ms |
| Mediaan aantal allocaties | 1 MB |
