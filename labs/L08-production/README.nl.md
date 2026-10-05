# Lab 8: productie (buiten de IDE)

Geen broncode, geen IDE, geen debugger: alleen een draaiend proces en de CLI-tools. Dichter bij wat een echt productie-incident je geeft, is er niet.

**Skills:** Een gecompileerd proces diagnosticeren waar je geen broncode van hebt, met alleen de CLI-tools.

**Mastery checkpoint:** Met alleen een draaiend proces een verdedigbare diagnose opleveren.

Geen `exercises/`/`solutions/`-opsplitsing hier: elk lab is een gecompileerde "mysterieuze service" zonder broncode in zicht, plus een `QUESTIONS.md` om in te vullen voordat je `ANSWERS.md` opent.

## Setup (eenmalig)
```powershell
./labs/L08-production/build-all.ps1     # publiceert elke lab's mysterieuze service naar <lab>/bin
```
Vereist [PowerShell 7+](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell) (`pwsh`), draait hetzelfde op Windows, Linux en macOS.

## Een lab aanpakken
1. Start de service van het lab in één terminal: `./labs/L08-production/<lab>/run.ps1 <args>` (de README van elk lab geeft de precieze argumenten; hij print zijn eigen pid).
2. Richt de CLI-tools vanuit een tweede terminal op die pid: `dotnet-counters`, `dotnet-trace`, `dotnet-gcdump`, `dotnet-dump`, afhankelijk van het lab.
3. Vul `<lab>/QUESTIONS.md` in **voordat** je `<lab>/ANSWERS.md` opent.

Labs: [L08-01-triage](L08-01-triage/README.md) · [L08-02-flame-graph](L08-02-flame-graph/README.md) · [L08-03-who-holds-memory](L08-03-who-holds-memory/README.md) · [L08-04-container-limits](L08-04-container-limits/README.md) · [L08-05-perf-gate](L08-05-perf-gate/README.md)

**Verder lezen:** [Leeslijst Lab 8](../../docs/READING-LIST.md#lab-8-beyond-the-ide)

## Counter-namen: .NET ≤ 8 vs. .NET 9+
Vanaf .NET 9 rapporteert `dotnet-counters monitor -p <pid> System.Runtime` de nieuwe `System.Runtime` **Meter** (`dotnet.*`-namen) in plaats van de oude EventCounters die eerdere runtimes en de meeste bestaande docs/blogposts gebruiken. Deze repo target .NET 10, dus je ziet de rechterkolom. De counters die deze labs daadwerkelijk gebruiken:

| Oude EventCounter-naam | Nieuwe (.NET 9+) meter-naam | Opmerkingen |
|---|---|---|
| `threadpool-queue-length` | `dotnet.thread_pool.queue.length` | Zelfde betekenis: werkitems die momenteel in de wachtrij staan. |
| `threadpool-thread-count` | `dotnet.thread_pool.thread.count` | Zelfde betekenis: worker threads die op dit moment bestaan. |
| `gc-heap-size` | `dotnet.gc.last_collection.heap.size` | Nu getagd per generatie (`gen0`/`gen1`/`gen2`/`loh`/`poh`) en wordt alleen bijgewerkt per collectie, niet op een timer. |
| `gen-0-gc-count`, `gen-1-gc-count`, `gen-2-gc-count` | `dotnet.gc.collections` | Nu één metriek in plaats van drie: de tag `gc.heap.generation` (`gen0`/`gen1`/`gen2`) maakt het onderscheid. |
| `alloc-rate` | `dotnet.gc.heap.total_allocated` | Oud was bytes/sec; nieuw is een cumulatief totaal sinds processtart, verschil tussen twee metingen geeft een rate, hetzelfde idee dat `Lab.cs` intern gebruikt. |
| `time-in-gc` | `dotnet.gc.pause.time` | Oud was een percentage over het sample-venster; nieuw is cumulatieve seconden gepauzeerd sinds start, verschil en deel door wall time voor een percentage. |
| `monitor-lock-contention-count` | `dotnet.monitor.lock_contentions` | Zelfde betekenis: cumulatief aantal lock contentions. |
| `exception-count` | `dotnet.exceptions` | Zelfde betekenis, nu getagd per exception-type (`error.type`). |

Toch de oude namen willen? `dotnet-counters monitor -p <pid> --counters EventCounters\System.Runtime` werkt nog steeds. Bron: [.NET runtime metrics (Microsoft Learn)](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/built-in-metrics-runtime).
