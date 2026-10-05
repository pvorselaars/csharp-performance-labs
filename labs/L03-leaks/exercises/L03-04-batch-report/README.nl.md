# L03-04 - Batch report

*Veel Drukte Om Geheugen*

## Symptoom
Een dashboard laat het geheugen van het proces tijdens een batch oplopen tot enkele honderden MB, dus iemand riep een **memory leak** uit en voegde na elke batch `GC.Collect()` toe om het "laag te houden". De batch is nu traag, en de harness laat ongeveer **150 gen2-collecties per run** zien. Het **behouden-na-een-volledige-GC**-cijfer is echter in beide gevallen vrijwel nul. Jouw taak is om *te bewijzen dat er geen lek is* en de schadelijke workaround te verwijderen.

## Doel
Zelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 14 ref-ms |
| Mediaan toegewezen | 90 MB |
| Mediaan aantal gen2-collecties | ≤ 10 |
| Behouden na een volledige GC | 0 MB |
