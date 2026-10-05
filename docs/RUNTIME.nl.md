# Runtime: hoe .NET jouw code uitvoert

>Deze pagina gaat over hoe de runtime jouw methods omzet in machinecode. De rest van .NET (de base class library, de thread pool, ASP.NET Core) komt elders aan bod: de thread pool en async in [Lab 4](../ROADMAP.md) en, onder belasting, [Lab 10](../ROADMAP.md).

Elke exercise in dit lab wordt getimed na een warm-up, en de harness heeft een `--cold`-modus zonder warm-up. Deze pagina legt uit waarom, want het verandert hoe je elk getal dat je tegenkomt moet lezen. Lees hem één keer in Lab 0; je komt erop terug in Lab 6 ([L06-07 warm-up curve](../labs/L06-hardware-runtime/exercises/L06-07-warmup-curve/README.md) is het praktische vervolg).


**Waar de getallen vandaan komen.** De metingen op deze pagina zijn genomen op de machine van de auteur (.NET 10.0.111, Linux, Release-build, vastgezet op twee CPU's) en laten de *vorm* van het effect zien. Bij jou zullen ze afwijken. Hoe elke fase werkt staat beschreven in de eigen documentatie van de runtime [[105]](READING-LIST.md#ref105), [[106]](READING-LIST.md#ref106), [[55]](READING-LIST.md#ref55), [[56]](READING-LIST.md#ref56), [[102]](READING-LIST.md#ref102); ga daarheen voor de details.

## Het idee: begin goedkoop, besteed meer aan wat heet blijkt te zijn
.NET-code wordt door de JIT (just-in-time compiler) omgezet naar machinecode de eerste keer dat een method draait, en de meeste methods in een programma draaien precies één keer of nooit meer. Elke method zo agressief mogelijk optimaliseren zou elke opstart traag maken. Daarom houdt de runtime meerdere manieren aan om dezelfde method uit te voeren en schuift hete code een ladder op:

| Fase | Wat het is | Snelheid van de code die het maakt |
|---|---|---|
| **ReadyToRun (R2R)** | Machinecode die vooraf gecompileerd is en meegeleverd wordt in de assembly [[102]](READING-LIST.md#ref102). Het framework zelf is zo gebouwd, dus het grootste deel van het framework heeft bij opstart nooit de JIT nodig | Behoorlijk, maar niet volledig geoptimaliseerd |
| **Tier 0** | De JIT met haast: compileert snel, doet vrijwel geen optimalisatie [[105]](READING-LIST.md#ref105) | Traagst |
| **Instrumented Tier 0** | Tier 0 plus counters die bijhouden hoe de method zich daadwerkelijk gedraagt: welke branches genomen worden, welke concrete types opduiken [[55]](READING-LIST.md#ref55) | Traag (de counters kosten iets) |
| **OSR** (on-stack replacement) | Voor een method die *al draait* in een lange loop: wisselt de draaiende loop halverwege de aanroep om naar geoptimaliseerde code, in plaats van te wachten op de volgende aanroep [[56]](READING-LIST.md#ref56) | Geoptimaliseerd |
| **Tier 1** | De volledig optimaliserende JIT, die het opgenomen profiel gebruikt wanneer er een is (*Dynamic PGO*, profile-guided optimisation) [[55]](READING-LIST.md#ref55) | Snelst |

Tiering uitzetten (`DOTNET_TieredCompilation=0`) slaat de ladder over: elke method wordt de eerste keer dat hij wordt aangeroepen met volledige optimalisatie gecompileerd. Dat is het label `FullOpts` dat je verderop in de listing ziet.

De afweging is precies het punt. Tier 0 krijgt je programma snel draaiend; Tier 1 krijgt het *snel* draaiend; het profiel uit de instrumented fase laat Tier 1 gokken maken (bijvoorbeeld "deze virtual call komt bijna altijd bij hetzelfde type uit") die een compiler zonder data niet zou kunnen maken.

## Hoe het eruitziet: één method, vijf manieren
Eén method met een hete loop (`Score`, hieronder), getimed in acht opeenvolgende batches van 20 calls, met de ladder aan en uit. Microseconden per batch; elk punt is de mediaan van vijf runs.

````mermaid

xychart
    title "Effecten van tiered compilation"
    x-axis "batch" [1, 2, 3, 4, 5, 6, 7, 8]
    y-axis "Uitvoeringstijd (µs)" 500 --> 4000
    line "Standaard" [1324,960,938,931,929,923,914,921]
    line "Geen PGO" [1109,881,866,872,874,871,881,873]
    line "Geen tiered compilation" [810,689,684,680,678,680,676,683]
    line "Vast in Tier 0" [1340,1290,1293,1310,1302,1295,1293,1285]
    line "Vast in instrumented Tier 0" [3589,3166,3120,3118,3094,3173,3076,3094]

````


- **De standaard is aanvankelijk niet de snelste.** Begint traag en verbetert, maar heeft Tier 1 nog niet bereikt binnen 8 batches (zo'n 8 ms runtime). Promotie duurt ongeveer 200 ms, dus in een langere run zakt het naar ongeveer 680 µs, hetzelfde als "geen tiered compilation".
- **Geen PGO slaat de counters over.** De ladder zonder de instrumented fase is iets sneller in het begin omdat hij niet betaalt voor de counters, maar zijn Tier 1-code krijgt geen profiel om mee te werken. Voor deze method maakt dat uiteindelijk niets uit (een simpele rekenkundige loop geeft PGO niets om op te gokken); bij virtual calls kan het wel uitmaken, en Lab 6 komt daarop terug.
- **Geen tiered compilation is vanaf het begin snel.** Elke method krijgt meteen de volledige optimizer bij de eerste aanroep. Batch 1 is iets trager omdat hij eenmalige kosten bevat, zoals het compileren van de method.
- **Ongeoptimaliseerde code is ongeveer twee keer zo traag.** Met promotie geblokkeerd (vast in gewone, *niet-instrumented* Tier 0), verlaat de method Tier 0 nooit en draait hij ongeveer 2 keer trager dan de volledig geoptimaliseerde versie. Dat is waar een programma op draait totdat de runtime besluit dat een method heet is.
- **De counters kosten meer dan de ontbrekende optimalisatie.** Vast in *instrumented* Tier 0 is dezelfde method ongeveer 4,5 keer trager dan geoptimaliseerde code. Voor een method met een loop is dit waar de standaard begint (de listing hieronder laat het zien), waarom de eerste batches van de standaard trager zijn dan die van Geen PGO.
- De "vast"-lijnen gebruiken instellingen die promotie blokkeren; het is een manier om Tier 0 *zichtbaar* te maken, geen ondersteunde configuratie, en de namen en effecten van de instellingen kunnen tussen .NET-releases veranderen.

Reproduceer het. Maak een scratch console-project (`dotnet new console -o Scratch`) en gebruik deze `Program.cs`:
```csharp
using System.Diagnostics;

long acc = 0;
int batches = args.Length > 0 ? int.Parse(args[0]) : 8;
var rows = new List<string>();
for (int b = 0; b < batches; b++)
{
    long t = Stopwatch.GetTimestamp();
    for (int k = 0; k < 20; k++) acc += Score(50_000);
    rows.Add(Stopwatch.GetElapsedTime(t).TotalMicroseconds.ToString("F0"));
}
if (acc == 42) Console.WriteLine(acc);           // gebruik het resultaat zodat het werk niet weggegooid kan worden
Console.WriteLine(string.Join(" ", rows));

static long Score(int n) { long s = 0; for (int i = 0; i < n; i++) s += (i * 7) % 101; return s; }
```
```bash
dotnet build -c Release
dotnet bin/Release/net10.0/Scratch.dll                                                          # alles aan
DOTNET_TieredPGO=0 dotnet bin/Release/net10.0/Scratch.dll                                       # geen instrumentatie
DOTNET_TieredCompilation=0 dotnet bin/Release/net10.0/Scratch.dll                               # tiering uit: direct volledige optimalisatie
DOTNET_TC_CallCounting=0 DOTNET_TC_OnStackReplacement=0 dotnet bin/Release/net10.0/Scratch.dll  # vast in Tier 0: geen call counting, geen OSR
DOTNET_TC_CallCountingDelayMs=7FFFFFFF DOTNET_TC_OnStackReplacement_InitialCounter=7FFFFFFF dotnet bin/Release/net10.0/Scratch.dll # vast in instrumented Tier 0: tellen start nooit
dotnet bin/Release/net10.0/Scratch.dll 600                                                      # lange run: kijk hoe het settelt
```
`DOTNET_*`-waarden worden gelezen als **hexadecimaal**: `7FFFFFFF` is de grootste waarde, en hem in decimaal schrijven (`2147483647`) geeft een ander getal dat promotie niet blokkeert.

(Op Linux houdt `taskset -c 0,2 dotnet …` de run op twee cores, wat de getallen stabieler maakt. Gebruik hiervoor geen enkele core: zie "Hoe lang duurt promotie?" hieronder.)

## Het zien gebeuren
De runtime kan elke method die hij compileert opsommen, met de bijbehorende fase:
```bash
DOTNET_JitStdOutFile=/tmp/jit.txt DOTNET_JitDisasmSummary=1 dotnet bin/Release/net10.0/<name>.dll 600
grep Score /tmp/jit.txt
```
Voor de run van 600 batches hierboven gaf dit (ingekort: omdat `Score` een local function is, is de volledige naam in het bestand `Program:<<Main>$>g__Score|0_0(int)`):
```text
Score(int) [Instrumented Tier0, IL size=27, code size=169]
Score(int) [Tier1-OSR @0x15 with Synthesized PGO, IL size=27, code size=80]
Score(int) [Instrumented Tier0, IL size=27, code size=169]
Score(int) [Tier1-OSR @0x15 with Synthesized PGO, IL size=27, code size=80]
Score(int) [Tier1 with Dynamic PGO, IL size=27, code size=56]
```
Lees de laatste regel: dat is waar de method uiteindelijk belandt. Hij ging van een instrumented, ongeoptimaliseerde body (169 bytes code) via een on-stack-replacement-versie (80) naar de uiteindelijke Tier 1-body die gecompileerd is met het opgenomen profiel (56).

Draai de gebouwde `.dll` direct, zoals hierboven. Gebruik je `dotnet run`, dan wordt de eigen opstartcode van de SDK in hetzelfde bestand gecompileerd en begraaft dat waar je naar op zoek bent.

Dezelfde schakelaar laat zien hoeveel werk ReadyToRun bespaart. Voor een triviaal programma als dit compileerde de standaardrun **73** methods met de JIT. Met `DOTNET_ReadyToRun=0`, wat de runtime voorgecompileerde code laat negeren, compileerde hij er **671**. Bijna alles wat het programma aanraakte in het framework was al gecompileerd. Hoeveel dat waard is in tijd is het onderwerp van [L06-07](../labs/L06-hardware-runtime/exercises/L06-07-warmup-curve/README.md).

## Hoe lang duurt promotie?
Niet meteen, en dat is met opzet. De runtime wacht tot de opstart afgerond lijkt voordat hij calls gaat tellen: er start een timer van 100 ms, en **elke nieuwe Tier 0-compilatie zet hem terug**; pas als hij rustig afloopt worden calls geteld, en een method heeft ongeveer 30 calls nodig om in aanmerking te komen [[106]](READING-LIST.md#ref106). Met de instrumented fase ertussen betekent dat **ruwweg 200 ms** voordat een hete method op Tier 1 zit. Dat zagen we precies. De loop-method hierboven viel bij 201 ms; in een tweede testprogramma (een kleine method die niet geïnlined wordt, zo'n twee miljoen keer aangeroepen per batch) kwam de val bij 196–205 ms, of de run nu niet vastgezet was, op twee cores of op vier.

**Op één CPU duurt de vertraging tien keer zo lang.** Vastgezet op één core (`taskset -c 0`) bereikte dat tweede programma Tier 1 pas na ongeveer **2.000 ms**, in alle drie de runs. `DOTNET_TC_DelaySingleProcMultiplier=1` instellen bracht het terug naar ongeveer 205 ms, waardoor we weten dat de multiplier de oorzaak is. Een container beperkt tot één CPU gedraagt zich waarschijnlijk hetzelfde, omdat de runtime één processor ziet; we hebben alleen met `taskset` getest, dus controleer het bij jouw omgeving. Alles wat je meet op een machine met één core heeft een veel langere warm-up nodig.

## Wat dit betekent voor de rest van het lab
- **Warm-up is geen vals spelen.** Eén run meten meet de opstart. De harness blijft de workload draaien tot ten minste twee runs *en* één seconde verstreken zijn, tot maximaal 60 runs, en timet pas dan. `--cold` slaat dat over zodat je de ladder kunt bekijken: `DOTNET_TieredPGO=0 dotnet run -c Release --project <exercise> -- --cold --runs 10`.
- **Een snelle workload kan toch gemeten worden vóór Tier 1.** De cap van 60 runs beëindigt de warm-up vroeg als elke run snel is. We hebben er één gecontroleerd: de L00-01-oplossing draait in ongeveer 0,7 ms, dus 60 warm-up-runs duren een paar tientallen milliseconden, ruim onder de ~200 ms die Tier 1 nodig heeft. De eigen listing laat zien dat `Feed.Build` de hele run afrondt als een `Tier1-OSR`-variant terwijl kleine methods zoals `FeedItem.get_Id` nog op Tier 0 zitten. De budgets hebben genoeg speling, dus de uitslagen worden er niet door beïnvloed, maar dit is waarom je speling laat in een budget dat je schrijft voor een erg snelle workload.
- **Profileer opgewarmde code.** Een profiel van een proces dat 200 ms heeft gedraaid is vooral een profiel van Tier 0-code en de JIT zelf. `--profile`-modus loopt daarom seconden door.
- **Dynamic PGO kan veranderen hoe de snelle code eruitziet.** Omdat Tier 1 het opgenomen profiel kan gebruiken, kunnen hete paden gecompileerd worden met gokken die een koude compiler niet zou maken, zoals guarded calls voor een virtual method die bijna altijd één type tegenkomt. [[55]](READING-LIST.md#ref55) heeft de details, en Lab 6 komt terug op dispatch.
- **De instellingen zijn om te leren, niet om te shippen.** Tiers uitzetten of promotie blokkeren is een manier om de ladder *te zien*. Draai hier geen productie mee uit; er zijn gedocumenteerde, ondersteunde instellingen voor productie [[105]](READING-LIST.md#ref105).

## Test jezelf
1. Waarom compileert de runtime niet elke method met de volledige optimizer bij de eerste aanroep?
2. Op de grafiek hierboven: wat kost de standaard je op een korte run, en wat levert hij je op bij een lange?
3. Waar is on-stack replacement voor, en waarom heeft een method met een lange loop dat nodig?
4. Waarom zou een benchmark die 50 ms draait op een machine met één core je bijna niets vertellen over de snelle code?

## Verder lezen
[[105]](READING-LIST.md#ref105) Compilation config settings · [[106]](READING-LIST.md#ref106) Tiered compilation design · [[55]](READING-LIST.md#ref55) Dynamic PGO design · [[56]](READING-LIST.md#ref56) OSR details · [[102]](READING-LIST.md#ref102) ReadyToRun · [[2]](READING-LIST.md#ref2) Toub's jaarlijkse *Performance Improvements in .NET*, waarvan de jaarlijkse edities deze features bespreken
