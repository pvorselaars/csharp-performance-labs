# L03-03 - Price ticker

*Ticker Tape Parade*

## Symptoom
Nadat 1.000 tickers zijn aangemaakt, gebruikt en losgelaten, blijft ongeveer **20 MB bereikbaar**, en komt de CPU daarna nooit meer helemaal tot rust. Er is hier geen static collection en geen event. Niets in *jouw* code houdt de tickers vast.

## Doel
Zelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 8 ref-ms |
| Mediaan toegewezen | 49 MB |
| Behouden na een volledige GC | ≤ 1 MB |

## Noot
Deze exercise heeft een **behouden-na-een-volledige-GC**-budget (de lek-gate). Hier is niets voor `Reset` om op te ruimen: het lek wordt *niet* vastgehouden door een static field die je kunt zien.
In de memory-snapshot-view van je profiler (dotMemory's Compare, VS's Memory Usage-diff, of een paar `dotnet-gcdump`-snapshots): neem een snapshot, draai `--profile --seconds 5`, neem nog een snapshot, diff ze, en kijk dan welk type domineert dat gegroeid is.
