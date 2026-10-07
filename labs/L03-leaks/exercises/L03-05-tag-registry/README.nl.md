# L03-05 - Tag registry

*Tikkertje, Jij Bent 'm*

## Symptoom
Een helper hangt per-object-metadata (view counts, labels) aan documenten via een lookup-table. Documenten worden aangemaakt, getagd, gebruikt en losgelaten, maar na 20.000 van zulke documenten blijft ongeveer **90 MB bereikbaar**. Niets in de app houdt de documenten vast; alleen de tag-helper heeft ze aangeraakt.

## Doel
Zelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 10 ref-ms |
| Mediaan toegewezen | 90 MB |
| Behouden na een volledige GC | 0 MB |

## Noot
Deze exercise heeft een **behouden-na-een-volledige-GC**-budget (de lek-gate). De harness roept `Workload.Reset()` aan voor elke run om *testopstelling* op te ruimen; die reset is niet de fix.
In de memory-snapshot-view van je profiler (dotMemory's Compare, VS's Memory Usage-diff, of een paar `dotnet-gcdump`-snapshots): neem een snapshot, draai `--profile --seconds 5`, neem nog een snapshot, diff ze, en kijk dan welk type domineert dat gegroeid is.
