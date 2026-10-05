# L02-03 - Pagina-renderer

*Page After Page*

## Symptoom
Het renderen van 6.000 pagina's duurt ongeveer **120 ms** en alloceert **570 MB**, en de harness rapporteert **~187 gen2-collecties** in
een enkele run. Dat is bijna één volledige collectie per 32 pagina's. Elke pagina heeft alleen een scratch-canvas van ongeveer 100 KB nodig.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 20 ref-ms |
| Mediaan gealloceerd | 0 MB |
| Mediaan gen2-collecties | ≤ 2 |

## Noot
Tijd wordt geschaald naar jouw machine; allocatie- en collectieaantallen niet.