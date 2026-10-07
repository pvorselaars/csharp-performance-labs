# perf
> Alleen Linux

**Beantwoordt:** wat doet de *CPU*? Instructies per cyclus, cache misses en branch misses: dingen die geen enkele .NET-profiler toont. Gebruik het als de profiler zegt "alle tijd zit in deze ene regel" en die regel er onschuldig uitziet.

## Installeren
```bash
sudo dnf install perf                                         # Fedora
sudo apt install linux-tools-common linux-tools-$(uname -r)   # Ubuntu/Debian
```
Zegt `perf` dat het geen toestemming heeft, controleer dan `cat /proc/sys/kernel/perf_event_paranoid`. Bij `2` (een gangbare standaard) kun je je eigen processen in user mode meten, wat hier alles dekt. Verlaag het met `sudo sysctl kernel.perf_event_paranoid=1` alleen als je meer nodig hebt.

## Commando's
Bouw eenmalig, en draai daarna de `.dll` direct, zodat je de exercise meet en niet de `dotnet run`-buildstap:
```bash
dotnet build -c Release labs/<lab>/exercises/<id>
perf stat -B dotnet labs/<lab>/exercises/<id>/bin/Release/net10.0/<id>.dll --profile --seconds 5
```
Draai hetzelfde commando voor `solutions/<id>` en vergelijk de twee.

## Hoe je het leest
- **IPC** = instructies ÷ cycli. Rond de 3 of meer is een CPU die op volle toeren draait. Onder 1 betekent dat hij vooral **wacht**, meestal op geheugen.
- **cache-misses:** vergelijk de exercise met de oplossing in plaats van het absolute getal te lezen. Een fix die het aantal misses tienvoudig verlaagt, verklaart een grote snelheidswinst.
- **branch-misses:** hetzelfde idee. Veel meer misses voor hetzelfde werk betekent dat de CPU steeds verkeerd gokt.
- De hele run wordt meegeteld, inclusief opstart en de harness. Een langere `--seconds` laat de workload domineren.

## Valkuilen
- **Hybride Intel-CPU's** (P-cores en E-cores) rapporteren elk event twee keer, als `cpu_core/...` en `cpu_atom/...`. Het percentage rechts is hoeveel van de run elk type geteld is. Lees de rij met het hoge percentage, of pin de run op de P-cores met [taskset](taskset.md).
- In VM's en containers tonen sommige events `<not supported>`: de hardware-teller is daar niet beschikbaar.
- `perf record` werkt ook op .NET (zet `DOTNET_PerfMapEnabled=1` zodat het JIT-gecompileerde methoden kan benoemen), maar de meeste frames zijn runtime-interne zaken. Voor "waar gaat de tijd naartoe" is [dotnet-trace](dotnet-trace.md) makkelijker te lezen.

## Documentatie
[perf wiki: tutorial](https://perfwiki.github.io/main/tutorial/) · [perf-stat man page](https://man7.org/linux/man-pages/man1/perf-stat.1.html)
