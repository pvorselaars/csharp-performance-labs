# L03-02 - Session cache

*Hotel California*

## Symptoom
Een session-lookup-cache maakt de app snel, maar na een batch van 40.000 requests blijft ongeveer **80 MB bereikbaar** na een volledige GC. Het verkeer in productie heeft geen bovengrens, en het geheugen van de box groeit gestaag tot de container wordt gekilled. Alleen *recente* sessies worden ooit opnieuw opgezocht.

## Doel
Zelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 28 ref-ms |
| Mediaan toegewezen | 202 MB |
| Behouden na een volledige GC | ≤ 6 MB |

## Noot
Deze exercise heeft een **behouden-na-een-volledige-GC**-budget (de lek-gate). De harness roept `Workload.Reset()` aan voor elke run om *testopstelling* op te ruimen; die reset is niet de fix.
In de memory-snapshot-view van je profiler (dotMemory's Compare, VS's Memory Usage-diff, of een paar `dotnet-gcdump`-snapshots): neem een snapshot, draai `--profile --seconds 5`, neem nog een snapshot, diff ze, en kijk dan welk type domineert dat gegroeid is.
