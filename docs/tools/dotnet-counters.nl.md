# dotnet-counters

**Beantwoordt:** wat doet het proces *in grote lijnen*, op dit moment? CPU, allocatiesnelheid, GC-tellingen en heap-grootte, thread pool-queue, lock-contentie, exceptions. Het is de goedkoopste eerste blik: vrijwel geen overhead, en achteraf niets te openen.

## Commando's
```bash
dotnet-counters ps                                     # lijst .NET-processen
dotnet-counters monitor -n <id>                        # live view van System.Runtime, elke seconde ververst
dotnet-counters monitor -n <id> --showDeltas           # voeg een kolom toe met de verandering sinds de vorige verversing
dotnet-counters monitor -n <id> --counters System.Runtime,Microsoft.AspNetCore.Hosting

# naar een bestand wegschrijven in plaats van kijken
dotnet-counters collect -n <id> --counters System.Runtime --format csv -o run.csv --duration 00:00:00:30
```
Druk `q` om `monitor` te stoppen. `--duration` is `dd:hh:mm:ss`.

## Hoe je het leest
Kijk er 10 tot 20 seconden naar voor je iets concludeert. Eén enkele verversing zegt weinig; de *trend* is het signaal.

| Je ziet | Dat wijst op |
|---|---|
| `dotnet.gc.heap.total_allocated` snel oplopend | veel allocatie: bekijk het met een allocatieprofiler |
| `dotnet.gc.collections` voor `gen2` oplopend | volledige collecties: grote objecten, of geheugen dat te lang blijft hangen |
| `dotnet.gc.last_collection.heap.size` groeit en daalt nooit | iets houdt objecten in leven: neem twee [gcdumps](dotnet-gcdump.md) en vergelijk ze |
| `dotnet.thread_pool.queue.length` boven nul, CPU laag | werk wacht op threads: geblokkeerde threads, zie [dotnet-stack](dotnet-stack.md) |
| `dotnet.thread_pool.thread.count` kruipt omhoog | de pool voegt threads toe omdat de bestaande geblokkeerd zijn |
| `dotnet.monitor.lock_contentions` oplopend | threads vechten om een `lock` |
| `dotnet.exceptions` oplopend | exceptions gebruikt als control flow, of een verborgen faallus |

Sommige metrics zijn opgesplitst via een tag, zoals `gc.heap.generation` (`gen0`, `gen1`, `gen2`, `loh`, `poh`). Lees de rijen onder de metric-naam, niet alleen de eerste.

## Valkuilen
- **De namen zijn veranderd in .NET 9.** De meeste blogposts gebruiken de oude (`alloc-rate`, `gc-heap-size`, `threadpool-queue-length`).
- **Sommige waarden zijn totalen sinds het proces startte**, geen snelheden (`total_allocated`, `collections`, `gc.pause.time`). Gebruik `--showDeltas`, of trek twee metingen van elkaar af.
- **De heap-grootte wordt alleen bijgewerkt na een GC.** Collect er niets, dan beweegt het getal niet, zelfs terwijl het geheugen groeit.

## Documentatie
[dotnet-counters (Microsoft Learn)](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/dotnet-counters) · [Built-in runtime metrics](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/built-in-metrics-runtime)
