# Profiling-gids

Een profiler kan je niet vertellen wat er mis is. Hij kan je alleen vertellen waar tijd en memory daadwerkelijk naartoe gingen, wat op zichzelf meestal al verrassend genoeg is. Dit is het spiekbriefje om daar een antwoord van te maken.

Alles hieronder is geschreven in termen van **modi** (sampling, tracing, timeline, allocaties, snapshots), niet in termen van één specifiek product, omdat elke echte profiler je deze in een of andere vorm geeft. Concrete stappen staan erbij voor Rider, Visual Studio en de gratis CLI-tools; gebruik wat je hebt.

## De lus
1. **Meet** een baseline (harness-output): een echt getal om te verslaan, geen vaag gevoel dat het traag is.
2. **Profileer** met de juiste modus: de tabel hieronder koppelt symptoom aan modus; gokken verspilt een run.
3. **Hypothese**: één zin, opgeschreven, *voordat* je code aanraakt: wat is er mis, en waarom denk je dat.
4. **Experimenteer**: verander precies één ding. Twee veranderingen tegelijk betekent dat je niet weet welke ertoe deed.
5. **Meet opnieuw**: dezelfde harness, dezelfde machine verder idle, geen shortcuts.
6. Verklaar *waarom* het getal bewoog. Kun je dat niet, dan had je geluk, en geluk generaliseert niet naar de volgende bug.

## Kies de modus bij het symptoom
| Symptoom | Modus die je nodig hebt | Waarom | Waar te vinden |
|---|---|---|---|
| Hoog CPU-gebruik, "het is traag" | **Sampling** | Lage overhead; laat zien waar CPU-tijd naartoe gaat | Rider dotTrace, VS CPU Usage, `dotnet-trace` |
| "Te vaak aangeroepen?" / exacte aantallen | **Tracing** | Exacte call-aantallen; vertekent timings. Gebruik voor aantallen, bevestig daarna met sampling | Rider dotTrace tracing mode |
| Welke *regel* in een hete method | **Line-by-line** | Precies maar zwaar; alleen nadat sampling het al heeft ingeperkt | Rider dotTrace line-by-line, VS per-regel hit counts |
| Laag CPU-gebruik maar traag / wachtend / threads | **Timeline** | Laat thread-states, blocking, GC-pauzes, I/O over tijd zien | Rider dotTrace timeline, VS Concurrency Visualizer, `dotnet-trace` → speedscope |
| Veel GC's, hoog allocatietempo | **Allocaties** | Allocatie-call-stacks | Rider dotMemory, VS .NET Object Allocation, `dotnet-counters` alloc-rate |
| Memory blijft groeien / "lek" | **Snapshots** (diff) + dominators | Wie houdt wat vast | Rider dotMemory compare, VS Memory Usage snapshots, `dotnet-gcdump` |
| Live, in productie | **Low-overhead CLI** | Geen IDE, lage overhead | `dotnet-counters`, `dotnet-trace`, `dotnet-gcdump` |

Elke profiler, hoe hij ook heet, biedt in werkelijkheid een subset hiervan aan. Weten wat elk van deze onder de motorkap daadwerkelijk doet, is wat je in staat stelt om op basis daarvan te kiezen in plaats van uit gewoonte.

- **Sampling.** Elke milliseconde of zo onderbreekt de profiler elke thread en noteert zijn huidige call-stack. Doe dat een paar duizend keer en de frames die het vaakst opduiken zijn degene die je daadwerkelijk CPU-tijd kosten. Het is statistiek, geen exacte meting (een heel snelle method die toevallig in de gaten tussen samples draait kan ondergeteld worden), maar de overhead is laag genoeg dat het programma nog op bijna normale snelheid draait. Dit is je standaard eerste zet voor "waarom is dit traag."
- **Tracing (instrumentatie).** De profiler herschrijft de code, of haakt in op de runtime, om bij *elke* method-entry en -exit een event vast te leggen. Je krijgt exacte call-aantallen en exacte timing per call, geen statistiek bij betrokken, maar de instrumentatie zelf kost tijd, vooral bij kleine, vaak aangeroepen methods, dus de timings die hij rapporteert kunnen opgeblazen zijn ten opzichte van de werkelijkheid. Goed voor "hoeveel keer wordt dit daadwerkelijk aangeroepen," slecht om de absolute milliseconden die hij toont te vertrouwen.
- **Line-by-line.** Hetzelfde idee als tracing, maar registreert een hit count en tijd per *broncoderegel* in plaats van per method. De preciesste van de drie, en de zwaarste: grijp er pas naar nadat sampling je al verteld heeft in welke method je moet kijken.
- **Timeline.** Een chronologisch overzicht van wat elke thread deed over wall-clock-tijd: draaiend, wachtend op een lock, geblokkeerd op I/O, gepauzeerd door de GC. Sampling beantwoordt "wat doet de CPU"; timeline beantwoordt "waarom zit de CPU stil." Grijp ernaar wanneer iets traag is maar het CPU-gebruik laag of normaal lijkt: dat is de signatuur van wachten, niet van rekenen, en sampling alleen zal je misleiden.
- **Allocaties.** Haakt in op de allocator om elke object-allocatie vast te leggen samen met de call-stack die hem veroorzaakte (en meestal in welke GC-generatie elk object uiteindelijk overleeft). Zo vind je *wie* het afval genereert, in tegenstelling tot timeline/sampling die je vertellen *dat* er garbage collection plaatsvindt.
- **Snapshots (heap diff).** Pauzeert het proces en loopt de hele levende objectgraaf af: een complete inventaris van elk object en wat ernaar verwijst. Neem een snapshot, laat wat werk draaien, neem er nog een, en diff ze: wat overblijft is of legitiem nog nodig, of een lek. Dit is de enige van de zes modi die gebouwd is om "waarom blijft memory groeien" te beantwoorden in plaats van "waarom is dit traag."

Ruwe volgorde van overhead, goedkoopst naar meest invasief: **sampling ≈ allocaties ≈ timeline < tracing < line-by-line.** Begin goedkoop, en grijp pas naar iets zwaarders zodra een goedkopere modus heeft ingeperkt waar je moet kijken.

## Een call tree lezen
- **Own/self time** = tijd in de eigen code van die method. **Total/cumulative** = inclusief callees.
- Sorteer op self time om de *leaf* te vinden die het werk doet; loop dan **omhoog** naar het eerste frame dat *jouw code* is.
- Het frame met de grootste total time is vaak gewoon het entry point. Het vertelt je niets.
- In een multi-threaded proces is de root total de som over alle threads, dus hij is groter dan de wall-clock-tijd van de run (en check de eenheid: `43.240 ms` is 43 s). Kijk naar de thread die je interesseert, meestal degene die `Main` draait.
- Een method die "er duur uitziet" en een method die *daadwerkelijk* duur is zijn twee verschillende dingen. Vertrouw de getallen.
- Vraag zowel "hoe vaak?" als "hoe lang?". 1 µs x 100 miljoen is een probleem; 10 ms x 1 niet.

## Valkuilen
- **Debug-builds** (JIT-optimizer uit) en een **gekoppelde debugger**: de getallen zijn zinloos. Profileer Release, via "Profile," niet "Debug." Let op: de **Profile**-launch-profile levert alleen de argumenten (`--profile --seconds 15`); hij kiest niet de build-configuratie, dus een IDE-run is Debug totdat je de solution-configuratie naar Release omzet. De harness drukt `Debug build detected` af wanneer dat gebeurt.
- **JIT-warm-up** en tiered compilation: de eerste run is trager. De harness warmt op; een echt profiel van een koud proces laat de opstart zien, wat een ander probleem is. ([RUNTIME.md](RUNTIME.md) legt de fases uit.)
- **Eén run is geen data.** Kijk naar meerdere; let op GC-veroorzaakte variantie.
- **Profiler-overhead** verandert het gedrag (tracing vooral). Bevestig een bevinding met een tweede, lichtere methode.
- **Optimaliseer niet wat het profiel niet laat zien.** En profileer opnieuw na elke fix: het beeld verandert.

## Workflows
### Rider-workflow
1. Run-configuratie: de **Profile**-launch-profile (argumenten `--profile --seconds 15`). Zet eerst de solution-configuratie op **Release**: de launch profile kan dat niet, en Debug-getallen zijn zinloos.
2. Start het profiel vanuit die configuratie, kies de modus (bewoording van het menu verschilt per versie).
3. Als de app klaar is, open je de snapshot. Begin bij **Hot Spots**/**Call Tree**, sorteer op own time.
4. Voor memory: neem snapshots op verschillende punten, gebruik **Compare**, inspecteer daarna **Dominators** / **Retention paths**.

**De totalen in een sampling-snapshot lezen.** De root-regel (`100% all calls`) telt de tijd van *elke thread* op, dus is meestal veel meer dan de tijd dat de app draaide. Voorbeeld: L00-01 profileren in profile mode (ongeveer 15 s) toonde een root van `43.240 ms`. Drie dingen om te weten:
- Rider groepeert cijfers met een scheidingsteken, dus `43.240 ms` is **43 seconden**, geen 43 milliseconden.
- Een .NET-proces heeft helper-threads naast degene die `Main` draait (8 threads in totaal op de Linux-machine van de auteur, waarvan er één al het werk doet), en sampling telt ook [threads die slapen of vastzitten in een lock](https://www.jetbrains.com/help/profiler/Basic_Concepts.html). Daarom is de root groter dan de run.
- Om een getal te krijgen dat je met de harness kunt vergelijken, vouw je de root uit en open je de **main thread** (of filter erop). Zijn totaal zou dicht bij de lengte van de run moeten liggen. Deel de tijd van `Feed.Build` op die thread door de regel `Done: N iterations` die de harness afdrukt en je krijgt de tijd per iteratie, ongeveer 190 ms voor L00-01 (hetzelfde als measure mode). De [sessie-opties](https://www.jetbrains.com/help/profiler/Profiler_Options.html) laten je ook kiezen of tijd waarin een thread niet werkt wordt meegeteld.

### Visual Studio-workflow
1. Stel de exercise in als startup project en kies de **Profile**-launch-profile (command-line-argumenten `--profile --seconds 15`, al ingesteld in `Properties/launchSettings.json`). Zet eerst de solution-configuratie op **Release**: de launch profile kan dat niet, en Debug-getallen zijn zinloos.
2. Debug → **Performance Profiler…**, vink **CPU Usage** en/of **.NET Object Allocation Tracking** aan, dan **Start** (niet F5, dat koppelt een debugger en geeft je zinloze getallen).
3. Als het klaar is, heeft de CPU Usage-view zijn eigen call tree: sorteer op **Self** (own time), hetzelfde idee als Rider's Hot Spots.
4. Voor memory: **Debug → Windows → Show Diagnostic Tools**, neem **Memory Usage**-snapshots voor/na, diff ze dan om te zien wat gegroeid is.

### CLI
Eén pagina per tool, met wat je typt en hoe je de output leest: [docs/tools](tools/README.md). De korte versie:
```bash
dotnet tool install --global dotnet-counters
dotnet tool install --global dotnet-trace
dotnet tool install --global dotnet-gcdump
dotnet tool install --global dotnet-dump

dotnet-counters ps                                   # vind het pid
dotnet-counters monitor -p <pid> System.Runtime      # cpu, alloc rate, gc-counts/heap-size, exception count, threadpool queue/threads
dotnet-trace collect -p <pid> --format Speedscope    # open daarna in https://www.speedscope.app
dotnet-gcdump collect -p <pid>                       # heap graph; open in VS/PerfView
dotnet-dump collect -p <pid>; dotnet-dump analyze <file>   # dan: dumpheap -stat, gcroot <addr>, threads, clrstack
```

> **Counter-namen zijn veranderd in .NET 9.** Op een .NET 9+ runtime rapporteert `dotnet-counters` `System.Runtime`-metrics als `dotnet.*`-namen (bijv. `dotnet.thread_pool.queue.length`, `dotnet.gc.collections`, `dotnet.monitor.lock_contentions`); de namen hieronder zijn die van .NET ≤ 8. Mapping-tabel: [labs/L08-production/README.md](../labs/L08-production/README.md).

Counters die je uit je hoofd moet kennen: `alloc-rate`, `gc-heap-size`, `gen-0/1/2-gc-count`, `time-in-gc`,
`exception-count`, `threadpool-queue-length`, `threadpool-thread-count`, `monitor-lock-contention-count`.

## Een hypothese testen

De exercise-harness beantwoordt *"is het snel genoeg, en nog correct?"*. Vanaf Lab 6 is dat niet genoeg - je moet ook *"waarom is A sneller dan B"* kunnen beantwoorden, bij voorkeur zonder gewoon op je gevoel te vertrouwen. Drie tools, goedkoopste eerst.

### 1. `perf stat`: hardware counters op Linux
BenchmarkDotNet's `[HardwareCounters]` is alleen-Windows (volgens de eigen docs), dus gebruik op Linux `perf`.
```bash
sudo dnf install perf                       # Fedora; andere distro's: linux-tools / perf
# perf heeft mogelijk nodig:  sudo sysctl kernel.perf_event_paranoid=1   (of draai met sudo)
dotnet build -c Release labs/L06-hardware-runtime/exercises/L06-01-matrix-walk
perf stat -e cycles,instructions,cache-references,cache-misses,branches,branch-misses \
  dotnet labs/L06-hardware-runtime/exercises/L06-01-matrix-walk/bin/Release/net10.0/L06-01-matrix-walk.dll --profile --seconds 5
```
Lees: **IPC** (instructions / cycles: laag betekent stalls), **cache-miss rate**, **branch-miss rate**. Vergelijk de exercise en de solution met dezelfde `--seconds`.
Niet elke counter is beschikbaar in VM's/containers; zie je `<not supported>`, dan is dat hardware-event daar niet blootgesteld.

### 2. JIT-disassembly: welke code kreeg je nu echt?
```bash
DOTNET_JitDisasm="Run" DOTNET_JitStdOutFile=/tmp/run.asm \
  dotnet labs/L06-hardware-runtime/exercises/L06-05-transform-batch/bin/Release/net10.0/L06-05-transform-batch.dll
```
`DOTNET_JitDisasm` accepteert method-namen/patronen (`Workload:Run`, `*Score*`). Het drukt de code af voor **elke tier** waarop een method gecompileerd is; kijk naar de laatste (Tier-1).
Het laat alleen methods zien *die de JIT compileert*. Roept jouw code iets in het framework aan (`span.Count`, `IndexOf`, `Sum`), dan zit de interessante loop in de framework-method, dus vraag om die naam (`SpanHelpers:*`, `*CountValueType*`). Framework-methods beginnen als voorgecompileerde ReadyToRun-code en duiken pas op zodra ze vaak genoeg zijn aangeroepen om herecompileerd te worden. Verschijnt er niets, draai dan in `--profile`-modus of zet `DOTNET_ReadyToRun=0`.
Dingen om naar te zoeken: block copies vóór calls (defensive copies), `call [reg+…]` (indirecte call, dus geen inlining), `vpcmpeqd`/`vpaddd` (SIMD), `cmov` (branchless) versus `jl`/`jge` (branch), bounds-check-`cmp`+`jae`-sequenties.

### 3. BenchmarkDotNet: betrouwbare micro-metingen
Gebruik het om *twee implementaties van een klein stukje code* te vergelijken, met statistiek, warm-up en procesisolatie al voor je geregeld.
Maak een scratch-project (voeg het **niet** toe aan de exercise-solution):
```bash
mkdir /tmp/bench && cd /tmp/bench
dotnet new console -n Bench && cd Bench
dotnet add package BenchmarkDotNet
```
`Program.cs`:
```csharp
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Walk>();

[MemoryDiagnoser]
[DisassemblyDiagnoser(maxDepth: 1)]          // laat de gegenereerde assembly naast de getallen zien
public class Walk
{
    const int N = 4096;
    readonly int[] _grid = new int[N * N];

    [Benchmark(Baseline = true)]
    public long ColumnMajor() { long s = 0; for (int c = 0; c < N; c++) for (int r = 0; r < N; r++) s += _grid[r * N + c]; return s; }

    [Benchmark]
    public long RowMajor() { long s = 0; for (int r = 0; r < N; r++) for (int c = 0; c < N; c++) s += _grid[r * N + c]; return s; }
}
```
Draai het: `dotnet run -c Release -- --filter '*'`. **Altijd Release, nooit onder een debugger.**

### Regels die een benchmark eerlijk houden
- **Geef het resultaat terug** (of consumeer het) zodat de JIT het werk niet kan weggooien.
- **Dezelfde input voor beide versies**; bouw data op in `[GlobalSetup]`, niet in de benchmark.
- Let op de kolommen **error en StdDev**: zijn ze groter dan het verschil, dan heb je geen resultaat.
- Eén benchmark per vraag; verander één ding.
- Draai op een rustige machine (sluit browsers; zet de CPU governor vast indien mogelijk).
- Getallen van één machine zijn niet overdraagbaar: draai opnieuw op de doelhardware.

Verder lezen: Akinshin, *Pro .NET Benchmarking* [[4]](./READING-LIST.md#ref4); de BenchmarkDotNet-diagnosers-docs [[54]](./READING-LIST.md#ref54); Bakhvalov, *Performance Analysis and Tuning on Modern CPUs* [[53]](./READING-LIST.md#ref53).

## Load, capaciteit en meetstrengheid

De ASP.NET-labs (9-14) meten een service onder gelijktijdige belasting, en een paar ideeën uit de wachtrijtheorie verklaren het meeste wat je daar zult zien. Ze verklaren ook waarom een loadtest je kan misleiden.

### De wet van Little en bezettingsgraad
- **De wet van Little:** `in-flight requests = doorvoer x gemiddelde latency`. Een service die 200 req/s verwerkt bij 50 ms gemiddelde latency heeft ongeveer 10 requests in flight. Verdubbelt de latency bij dezelfde doorvoer, dan zijn er twee keer zoveel in flight, en dat is waar de thread-pool-queue, de connection pool en memory groeien.
- **Bezettingsgraad (utilisation)** is het deel van de capaciteit van een resource dat bezet is. Wachtrijvertraging groeit eerst langzaam, dan scherp: bij een simpele single-server-queue schaalt de wachttijd ruwweg met `u / (1 - u)`, dus van 50% naar 90% bezet vermenigvuldigt de wachttijd met ongeveer 9, en bij 95% met ongeveer 19. Daarom lijkt de latency prima totdat de service *bijna* vol is, en dan stort hij in. Plan niet om een gedeelde resource (CPU, DB-pool, thread pool) dicht bij 100% te draaien.
- **Saturatie** is het punt waarop een resource geen vrije capaciteit meer heeft: werk komt minstens zo snel binnen als het bediend kan worden, dus vormt zich een queue die blijft groeien. Een resource kan verzadigd zijn ruim onder 100% CPU. Een pool van 10 DB-connecties is verzadigd wanneer alle 10 uitgecheckt zijn en aanroepers wachten, en een thread pool is dat wanneer zijn workqueue niet leeg is en niet afneemt, zelfs als de cores idle zijn. Hoe dat er van buitenaf uitziet:
  - De doorvoer stopt met stijgen naarmate je belasting toevoegt, en vlakt af (of daalt) terwijl de latency blijft klimmen.
  - Een queue groeit: pool-wachttijd, `threadpool-queue-length`, pending requests, een stijgend in-flight-aantal (weer de wet van Little).
  - De bezettingsgraad van één resource zit dicht bij zijn limiet terwijl de andere comfortabel zijn.

  Bezettingsgraad vertelt je hoe druk een resource is, en saturatie vertelt je of er al werk op hem wacht. Check beide voor elke resource (CPU, memory, thread pool, connection pool, disk/netwerk, downstream service), en zoek naar *queues* in plaats van alleen bezettingspercentages. Saturatie is ook wat de twee manieren om traag te zijn van elkaar scheidt: een onverzadigde service is traag omdat elke request te veel werk doet (profileer de code), een verzadigde is traag omdat requests wachten (vind de queue en de resource waarop hij wacht).
- **De bottleneck bepaalt het plafond.** De doorvoer wordt begrensd door de meest verzadigde resource. Capaciteit ergens anders toevoegen verandert niets, dus vind eerst de verzadigde (CPU, poolgrootte, een downstream-afhankelijkheid, een lock).
- **Zodra aankomsten de capaciteit overschrijden groeit de queue onbegrensd**, en stijgt de latency zolang de overbelasting duurt. Retries en timeouts maken het erger door juist belasting toe te voegen op het moment dat er niets te sparen is (de L12-retry-storm).

### Wat de load driver je kan voorliegen
- **Closed loop versus open loop.** Een closed-loop-driver (N users, elk stuurt de volgende request zodra de vorige terugkomt; `WebRig.Drive`) vertraagt wanneer de service vertraagt, dus overbelast hij hem nooit en verbergt hij queueing. Een open-loop-driver (requests komen binnen met een vast tempo, wat de service ook doet; `WebRig.DriveOpen`) is hoe echt verkeer eruitziet, en is degene die saturatie blootlegt. Gebruik closed loop om de best-case-latency te vinden, open loop om te vinden waar het breekt.
- **Coordinated omission.** Als een stall ervoor zorgt dat de driver requests overslaat die hij had moeten sturen, komen die ontbrekende trage requests nooit in de statistiek terecht en lijkt de staart beter dan hij was. Meet latency vanaf het moment dat de request *verschuldigd* was, niet vanaf wanneer hij daadwerkelijk verstuurd werd. `DriveOpen` doet dit.
- **Gemiddeldes verbergen de staart.** Rapporteer p50, p99 en max, en het aantal requests achter elk ervan. Een p99 uit 100 samples is één request. Percentielen kun je niet middelen over runs of instanties; voeg in plaats daarvan de ruwe samples of histogrammen samen.
- **Warm up, meet daarna.** Gooi het begin weg (JIT, caches, poolgroei) en vermeld hoe lang de run duurde. Korte runs onderbemonsteren zeldzame events zoals gen2-GC's.

### Meetchecklist
1. Formuleer de vraag en het getal dat hem beantwoordt (p99 bij 500 req/s, niet "sneller").
2. Herhaal de run, en vergelijk de spreiding met het verschil dat je probeert te detecteren. Is de ruis groter dan het effect, dan heb je geen resultaat.
3. Verander één ding, houd de workload gelijk, en noteer de machinestatus (belasting, energiemodus, Release versus Debug).
4. Controleer dat het resultaat ook *correct* is, niet alleen snel (de checksum van de harness), en dat errors en timeouts geteld worden in plaats van weggelaten.
5. Bevestig de bevinding met een tweede methode voordat je ernaar handelt (zie [Valkuilen](#valkuilen)).

Verder lezen: Gil Tene's talk "How NOT to Measure Latency" (coordinated omission); Google, *Site Reliability Engineering*, het hoofdstuk over het monitoren van gedistribueerde systemen (latency en de four golden signals).
