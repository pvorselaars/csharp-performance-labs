# L01-02 - Contactimport

*Contactsport*

## Symptoom
Het importeren van 20.000 contactrijen (zo'n 12.000 unieke personen, met herhalingen en wisselende
hoofdlettergebruik) duurt **meer dan een seconde**. Eén CPU-core staat op 100%; het geheugen ziet er prima
uit. De import van gisteren, 2.000 rijen, duurde een paar milliseconden en niemand stond erbij stil.

## Doel
Hou exact dezelfde resultaten aan (eerste voorkomen wint, e-mail hoofdletterongevoelig vergeleken) en haal:

| Budget | Waarde     |
|---|-----------|
| Mediane tijd | 59 ref-ms |
| Mediaan aantal allocaties | 16 MB     |

## Regels
Zoals altijd: bewerk alleen `Workload.cs`, hypothese in `templates/LAB-LOG.md` voordat je de code verandert,
hints één tegelijk.
Let op het interessante punt van deze exercise: het **allocatiegetal** is hier *niet* het probleem. Welk
budget faalt, vertelt je naar welk type profiler je moet grijpen.
