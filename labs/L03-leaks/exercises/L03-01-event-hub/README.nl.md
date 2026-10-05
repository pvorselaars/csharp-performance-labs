# L03-01 - Event hub

*Heengegaan, maar niet vergeten*

## Symptoom
Na een batch van 2.000 kortlevende widgets blijft ongeveer **20 MB bereikbaar**, zelfs na een volledige GC, en wordt elke batch trager dan de vorige. Elke widget werd "losgelaten" zodra hij gebruikt was, en niets anders in de code die je ziet verwijst er nog naar.

## Doel
Zelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 11 ref-ms |
| Mediaan toegewezen | 49 MB |
| Behouden na een volledige GC | ≤ 1 MB |

## Noot
Deze exercise heeft een **behouden-na-een-volledige-GC**-budget (de lek-gate). De harness roept `Workload.Reset()` aan voor elke run om *testopstelling* op te ruimen; die reset is niet de fix.
In de memory-snapshot-view van je profiler (dotMemory's Compare, VS's Memory Usage-diff, of een paar `dotnet-gcdump`-snapshots): neem een snapshot, draai `--profile --seconds 5`, neem nog een snapshot, diff ze, en kijk dan welk type domineert dat gegroeid is.
