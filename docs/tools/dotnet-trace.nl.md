# dotnet-trace

**Beantwoordt:** waar gaat de tijd naartoe? Het neemt stack-samples (en runtime-events) van een draaiend proces op in een bestand, dat je daarna in een viewer opent. Het is het CLI-equivalent van een sampling profiler.

## Commando's
```bash
# neem 10 seconden op, en schrijf ook een speedscope-bestand naast de .nettrace
dotnet-trace collect -n <id> --duration 00:00:00:10 --format Speedscope -o run.nettrace

# snelle tekstsamenvatting van de heetste methoden, geen viewer nodig
dotnet-trace report run.nettrace topN -n 10

# start het programma onder de tracer in plaats van te koppelen (vangt ook de opstart)
dotnet-trace collect --format Speedscope -o run.nettrace -- dotnet path/to/app.dll --profile --seconds 10

# allocatie- en GC-events in plaats van CPU-samples
dotnet-trace collect -n <id> --profile gc-verbose --duration 00:00:00:10

dotnet-trace list-profiles                             # welke events de andere --profile-waarden opnemen
```
Druk Enter of `Ctrl+C` om vroegtijdig te stoppen. `--duration` is `dd:hh:mm:ss`.

## Het resultaat bekijken
- **speedscope:** open [speedscope.app](https://www.speedscope.app) en sleep het `.speedscope.json`-bestand erop. Het draait in je browser; het bestand wordt niet geüpload. Gebruik **Left Heavy** om te zien waar de tijd naartoe ging (breedste balk = meeste tijd), en **Sandwich** om de callers en callees van één methode te zien.
- **Rider / dotTrace, Visual Studio, PerfView:** open de `.nettrace` direct.
- **Geen viewer:** `dotnet-trace report ... topN` drukt de top methoden af naar exclusieve tijd.

## Hoe je het leest
- Zoek eerst **je eigen** code (de namespace van de exercise), en kijk dan wat die aanroept. Framework-methoden bovenaan de lijst (`Buffer.Memmove`, `String.Concat`, `Monitor.Enter`) zijn meestal de *kosten*; het frame in jouw code er vlak onder is de *oorzaak*.
- **De samples bestrijken elke thread**, en wachtende threads worden ook gesampled. In een snelle test op een exercise die al zijn werk op één thread doet, stonden `Memmove` (het echte werk) *en* `WaitHandle.WaitOne` (een inactieve helper-thread) op hetzelfde percentage. Filter op de thread die `Workload.Run` draait, of negeer frames die duidelijk aan het wachten zijn.

## Valkuilen
- **Koppel pas na de warm-up.** De eerste seconden zijn JIT-compilatie en tiering, wat de harness nooit meet. Profile-modus loopt al door, dus wacht een paar seconden voor je begint met opnemen.
- **Traces groeien snel:** een trace van 4 seconden van een drukke exercise was ongeveer 11 MB. Houd `--duration` kort.
- Op Linux legt `collect-linux` ook kernel-events vast, maar heeft root nodig. Gewone `collect` dekt alles in dit lab.

## Documentatie
[dotnet-trace (Microsoft Learn)](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/dotnet-trace) · [speedscope](https://github.com/jlfwong/speedscope#usage)
