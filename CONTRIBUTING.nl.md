# Bijdragen

Al het andere in deze repo is geschreven voor iemand die een exercise *oplost*. Dit bestand is voor iemand die er een *toevoegt*, een bug in een bestaande fixt, of anderszins het lab zelf wijzigt.

## Een nieuwe exercise toevoegen

### Naamgeving
`L<lab>-<NN>-<slug>` (bv. `L02-07-route-stats`). De eindbaas van een lab is `L<lab>-boss-<slug>` in plaats van een nummer. Lab 8 is de uitzondering: de exercises daar zijn gecompileerde "mysterieuze services" (`L08-01-triage`, …), geen exercises; zie [labs/L08-production/README.md](labs/L08-production/README.md) als je daar juist zo'n exercise aan toevoegt.

### Verplichte bestanden
Elke exercise is een gekoppeld paar projecten:

```
labs/<lab>/exercises/<id>/
    README.md        symptoom + budgettabel, NIET de diagnose (en geen bonusinfo: die noemt de fix, dus die hoort in SOLUTION.md). Zie een bestaande exercise voor de vorm.
    HINTS.md          oplopende hints, één <details>-blok per hint, goedkoopste tool eerst.
    Workload.cs       de bewust trage/kapotte implementatie.
    Program.cs        roept Lab.Run(new LabSpec(...), args) aan: de enige bron van waarheid voor budgets en checksum.
    <id>.csproj       OutputType Exe, ProjectReference naar src/PerfLab.Harness/PerfLab.Harness.csproj.
    Properties/launchSettings.json   'Measure' (geen args, als eerste zodat een kale `dotnet run` meet) en 'Profile' (`--profile --seconds 15`) profielen; kopieer van een bestaande exercise. Een launch profile kan geen build-configuratie kiezen, dus probeer Release er niet in te coderen.

labs/<lab>/solutions/<id>/
    SOLUTION.md       wat het profiel laat zien, grondoorzaak, de fix, "extra credit"- en "ga verder"-vragen, verdere literatuur.
    Workload.cs       de gefixte implementatie, hetzelfde publieke oppervlak, dezelfde checksum als de exercise.
    Properties/launchSettings.json   hetzelfde als die van de exercise.
    <id>.csproj       zelfde vorm, maar koppelt de Program.cs van de exercise in plaats van die te dupliceren:
                      <Compile Include="..\..\exercises\<id>\Program.cs" Link="Program.cs" />
```

Die `<Compile Include>`-koppeling garandeert dat de exercise en zijn solution tegen exact dezelfde budgets en checksum worden beoordeeld; kopieer `Program.cs` nooit naar de solution-map in plaats van hem te linken.

### Budgets en checksum instellen
`Program.cs` bouwt een `LabSpec`, zie [`src/PerfLab.Harness/Lab.cs`](src/PerfLab.Harness/Lab.cs) voor wat elk veld betekent (volledig gedocumenteerd met XML-comments). Kort samengevat:
- **`ExpectedChecksum`**: draai de *gefixte* `Workload.cs`, lees de waarde die hij teruggeeft, plak die hier in. Zowel de exercise als de solution moeten dezelfde checksum opleveren: dat is wat "je hebt de output correct gehouden" betekent.
- **`MaxMetrics[Metrics.Time]`**: plak niet zomaar het getal dat je gemeten hebt erin, jouw machine is vrijwel zeker niet de referentiemachine, dus een ruwe meting bakt de afwijking van jouw machine in voor iedereen die de gate ergens anders draait (zie de sectie "The harness" in README.md voor wat de schaling precies doet):
  1. Draai een willekeurige, al gebouwde exercise één keer, bv. `dotnet run -c Release --project labs/L01-hot-spots/exercises/L01-01-invoice-export`, en lees de afgedrukte regel `Machine factor X.XXx vs. reference`. De spin-loop die daarbij getimed wordt is onafhankelijk van de workload, dus elke exercise geeft dezelfde factor.
  2. Draai je nieuwe solution en lees de ruwe **mediane tijd** uit de resultatentabel, niet de "budget"-regel, die is al geschaald.
  3. Deel: `reference_ms = raw_median_ms / factor`. Dat is het getal dat je in `MaxMetrics[Metrics.Time]` zet.
  4. Tel daar nog marge bij op.

  Als de tijd van de workload gedomineerd wordt door een vaste wall-clock-wachttijd in plaats van CPU-werk (`Task.Delay`, `Thread.Sleep`, een echte request-roundtrip via `WebRig.Drive`), zet dan `ScaleTime: false` in plaats van te schalen: die wachttijd wordt niet sneller op een snellere CPU, dus geen enkele op deze manier berekende factor zou daarvoor kloppen. Schrijf in dat geval het ruwe gemeten getal er direct in, met marge.

  Diezelfde `ScaleTime: false` geldt als de workload gedomineerd wordt door de native heap-allocator van het OS (`Marshal.AllocHGlobal`/`FreeHGlobal`, of andere unmanaged allocaties), maar om een andere reden: de kosten van de allocator zijn helemaal geen kwestie van CPU-snelheid, ze hangen af van welk codepad een gegeven blokgrootte en -patroon raakt (de heap manager/`VirtualAlloc` van Windows, glibc `malloc`/`mmap` op Linux, de zone-allocator van macOS), en die verschillen *kwalitatief* tussen platformen, niet met één enkele snelheidsverhouding die een factor zou kunnen vangen. Bekende makke: herhaalde `AllocHGlobal`/`Free`-calls van 1 MB (`L07-02-phantom-leak`) maten een stabiele ~35 ms op Windows tegen een budget van ~6 ms dat elders was gekalibreerd, ruwweg een platformverschil van 6x voor dat exacte patroon. Er is geen portabele manier om dit te kalibreren, dus geef het ruwe getal royaal marge (2-3x wat je meet) in plaats van te proberen het aan te scherpen, en waar de exercise een echte correctheidsgate beschikbaar heeft (retained memory, een gerapporteerde metric zoals `privateMB`), geef die de voorkeur boven het tijdbudget voor de werkelijke onderscheiding, want dat is meestal toch waar de exercise eigenlijk op test.
- **`MaxMetrics[Metrics.Alloc]`**: zelfde idee, maar **niet** geschaald: allocatiebudgets zijn absoluut.
- Elk budget - `Metrics.Time` en `Metrics.Alloc` inbegrepen - leeft in die ene `MaxMetrics`-dictionary (zie `src/PerfLab.Harness/Metrics.cs` voor de ingebouwde namen: `Time`, `Alloc`, `P99`, `Cpu`, `Gen2`, `Retained`). Voeg alleen degene toe die de specifieke les van de exercise moet gaten (LOH/leak/concurrency-niveaus, of een workload met vaste vertraging) - een exercise mag `Metrics.Time`/`Metrics.Alloc` helemaal weglaten voor pure verkenning, zonder iets om te slagen of falen. Stel `Reset`, `MaxFirstRunMs` of `ScaleTime` op dezelfde manier alleen in als de exercise ze nodig heeft; ze op hun standaardwaarde laten betekent "niet gegate" (of, voor `ScaleTime`, "normaal geschaald").

De trage (exercise-)versie moet op **minstens één** budget falen; de gefixte (solution-)versie moet **ze allemaal** halen, met een identieke checksum. Die koppeling is wat `scripts/perf-gate.ps1` automatisch controleert (zie Valideren, hieronder). Het is ook het hele punt van de exercise, dus sla het niet over om dit eerst zelf met de hand te verifiëren.

### Het project registreren
Voeg een `<Project Path="...">`-regel toe aan de juiste `<Folder>` in **beide** solution-bestanden:
- `PerfLab.slnx` → pad onder `labs/<lab>/exercises/<id>/<id>.csproj`
- `PerfLab.Solutions.slnx` → pad onder `labs/<lab>/solutions/<id>/<id>.csproj`

Voeg er daarna een rij voor toe aan de exercise-tabel in de `labs/<lab>/README.md` van dat lab.

### Stijl
De bestaande exercises delen één stem: het symptoom wordt beschreven in termen van wat je *waarneemt*, nooit wat er mis is met de code; `README.md` gebruikt nooit het woord "bug"; hints escaleren van "welke tool" naar "welke regel" zonder ooit gewoon de fix te noemen. Lees twee of drie bestaande exercises in het doellab voordat je een nieuwe schrijft; die stem matchen is belangrijker dan mechanisch een template volgen.

## Je wijziging valideren
```powershell
./scripts/perf-gate.ps1 <prefix>   # bv. ./scripts/perf-gate.ps1 L02, of het L-nummer van één exercise
```
Vereist [PowerShell 7+](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell) (`pwsh`), dat op Windows, Linux en macOS hetzelfde draait; geen bash/WSL nodig.
Bouwt beide `.slnx`-solutions in Release, en draait dan voor elk exercise/solution-paar en controleert: elke solution sluit af met `0` (geslaagd), elke exercise sluit af met `1` of `2` (faalt zijn eigen budgets: een trage versie die per ongeluk slaagt is precies wat dit script moet opvangen). Een mismatch print `SURPRISE` en het script sluit af met een niet-nul code.

Dit is ook wat [.github/workflows/perf-gate.yml](.github/workflows/perf-gate.yml) in CI draait op PR's die `src/**`, `labs/**`, `solutions/**`, of `Directory.Build.props` raken.

## Overige wijzigingen
- **Docs** (`README.md`, `ROADMAP.md`, `docs/`, `templates/`): gebouwd met MkDocs. `./scripts/build-docs.ps1` (of `-Serve` voor lokale preview) bouwt onder `--strict`, wat faalt op gebroken interne links; draai dit voordat je een PR opent die markdown raakt.
- **De harness** (`src/PerfLab.Harness`, `src/PerfLab.Harness.Web`): houd XML-doc-comments actueel op de publieke API (`LabSpec`, `Lab.Run`, `Lab.RecordLatency`, `Lab.Report`, de publieke members van `WebRig`); dat is het eerste wat IntelliSense een exercise-auteur toont.

### Vertalingen
Elke docpagina kan een Nederlandse vertaling hebben die er vlak naast leeft,
genaamd `<page>.nl.md` (bv. `README.nl.md` naast `README.md`). Pagina's zonder
`.nl.md` vallen automatisch terug op de Engelse versie, dus vertalingen kunnen
incrementeel worden toegevoegd, pagina voor pagina. Vertaal de betekenis, niet
woord voor woord: houd je aan dezelfde stijlregels als hierboven (bv. dat
`README.md`-bestanden van exercises nooit de bug benoemen), ook als dat
betekent dat je van een letterlijke vertaling afwijkt.

Als je PR een nieuwe pagina toevoegt, of een bestaande wijzigt die al een
`.nl.md`-tegenhanger heeft, voeg dan de Nederlandse vertaling toe of werk hem
bij zodat de twee niet uit elkaar gaan lopen. Niet vereist om een PR gemerged
te krijgen, maar wel gewaardeerd.
