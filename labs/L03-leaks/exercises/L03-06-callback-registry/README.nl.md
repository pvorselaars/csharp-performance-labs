# L03-06 - Callback registry

*Bel Ons Niet*

## Symptoom
3.000 kleine callbacks worden geregistreerd in een langlevende registry. Elke callback geeft één getal terug, maar na de run blijft ongeveer **60 MB bereikbaar na een volledige GC**: 20 KB per callback. De registry is hier met opzet begrensd (testopstelling ruimt hem op). De vraag is waarom elke entry zo *groot* is.

## Doel
Zelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 8 ref-ms |
| Mediaan toegewezen | 144 MB |
| Behouden na een volledige GC | ≤ 2 MB |
