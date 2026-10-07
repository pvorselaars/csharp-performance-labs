# L07-04 - Whodunit

*It Wasn't the Butler*

## Symptoom
Een lookup-loop controleert 2 miljoen keys tegen een index van 50.000 entries en duurt ongeveer **een halve seconde**: ruim 200 ns per lookup, voor een dictionary...

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 15 ref-ms |
| Mediaan toegewezen | 0 MB |

## Boss fight-regels
- **Alleen het symptoom.** Er zijn meerdere defecten en het oplossen van het ene legt meestal het volgende bloot; meet opnieuw na elke verandering.
- Hints zijn met opzet generiek. Gebruik `templates/POSTMORTEM.md` en schrijf de post-mortem *voordat* je de oplossing leest.
