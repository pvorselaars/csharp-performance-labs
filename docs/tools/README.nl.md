# CLI-tools

Korte gidsen bij de command-line tools die dit lab gebruikt: wat elke tool beantwoordt, de commando's die je daadwerkelijk typt, en hoe je leest wat eruit komt. Ze zijn gratis, werken zonder IDE, en de meeste werken op elk OS. Je hebt ze nodig in Lab 8, en ze zijn een goede tweede mening in elk ander lab.

Voor *welk soort* meting een symptoom nodig heeft (sampling, tracing, snapshots, ...), begin bij de [profiling guide](../PROFILING-GUIDE.md). Deze pagina's zijn het "hoe typ ik het"-vervolg daarop.

## Welke tool bij welke vraag
| Vraag | Tool | OS |
|---|---|---|
| Is het proces bezig, aan het alloceren, aan het collecten, aan het wachten in een queue? (de eerste blik) | [dotnet-counters](dotnet-counters.md) | elk |
| Waar gaat de tijd naartoe? | [dotnet-trace](dotnet-trace.md) | elk |
| Wat staat er op de heap, en wat houdt het vast? | [dotnet-gcdump](dotnet-gcdump.md) | elk |
| Alles in memory, offline geïnspecteerd (threads, locks, roots) | [dotnet-dump](dotnet-dump.md) | elk |
| Wat doet elke thread *op dit moment*? | [dotnet-stack](dotnet-stack.md) | elk |
| Cache misses, branch misses, instructies per cyclus | [perf](perf.md) | Linux |
| Hoeveel system calls, en welke? | [strace](strace.md) | Linux |
| Hoeveel TCP-verbindingen, in welke staat? | [ss](ss.md) | Linux |
| Draaien op minder cores, of steeds op dezelfde cores | [taskset](taskset.md) | Linux |

## Installeer de .NET-tools eenmalig
```bash
dotnet tool install --global dotnet-counters
dotnet tool install --global dotnet-trace
dotnet tool install --global dotnet-gcdump
dotnet tool install --global dotnet-dump
dotnet tool install --global dotnet-stack
```
Later werkt `dotnet tool update --global <name>` er één bij. Kan de shell ze na installatie niet vinden, voeg dan `~/.dotnet/tools` (Linux/macOS) of `%USERPROFILE%\.dotnet\tools` (Windows) toe aan je `PATH`.

## Richt een tool op een exercise
Al deze tools koppelen aan een **draaiend proces**, en een normale exercise-run is binnen een seconde voorbij. Gebruik de profile-modus van de harness, die de workload laat doorlopen:

```bash
# terminal 1: houd de exercise een minuut bezig
dotnet run -c Release --project labs/<lab>/exercises/<id> -- --profile --seconds 60

# terminal 2: koppel op naam (het exercise-id)...
dotnet-counters monitor -n <id>
# ...of zoek eerst het pid op
dotnet-counters ps
dotnet-counters monitor -p <pid>
```

- **Elke .NET-tool hier neemt `-n <name>` of `-p <pid>`.** De naam is het exercise-id (`L1-01-invoice-export`) als je hem start met `dotnet run`. Start je de `.dll` met `dotnet <file>.dll`, dan heet het proces `dotnet`, dus gebruik het pid.
- `dotnet-counters ps` toont **elk** .NET-proces, IDE's en build servers inbegrepen. Zoek het exercise-id in de command-line-kolom.
- Lab 8-exercises drukken bij het starten hun eigen pid af, dus daar kun je `ps` overslaan.
- De tools zien alleen processen die als dezelfde gebruiker draaien. Start de exercise niet met `sudo`.
