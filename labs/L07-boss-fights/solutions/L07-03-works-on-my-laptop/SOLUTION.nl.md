# L07-03 - Oplossing

## Wat het profiel laat zien
- **Harness:** `committedMB` is ongeveer 16 MB per core: 33 (2 cores), 65 (4), 352 (20). De fix is 3 MB in alle drie de gevallen.
- **GC:** één heap per core, en maar 15 gen0-collecties per run (de fix: 1.880), omdat elke heap een groot allocatiebudget heeft. `GC.GetConfigurationVariables()["HeapCount"]` laat het heap-aantal zien waarmee de GC is geconfigureerd, wat tevens het *maximum* is zodra dynamische aanpassing aanstaat, niet het aantal dat daadwerkelijk in gebruik is.

## Grondoorzaak
Server-GC met dynamische aanpassing uitgezet en geen cap op het heap-aantal, op een machine met veel cores. De GC dimensioneert zichzelf voor een grote, drukke server: veel heaps, elk met een groot gen0-budget, dus het committed memory loopt op ook al is de levende data minimaal.

## Fix
Verwijder de geplakte regel `System.GC.DynamicAdaptationMode=0`. Dynamische aanpassing (DATAS) staat standaard aan voor Server-GC vanaf .NET 9, en dimensioneert de heaps op wat de app daadwerkelijk gebruikt.

Workstation-GC en een heap-aantal van 1 slagen ook. Het heap-aantal cappen op 4 helpt alleen op machines met meer dan 4 cores, wat weer dezelfde "works on my laptop"-valkuil is: een fix afgestemd op één machine. Welke je ook kiest, meet ook de doorvoer.

## Lessen
1. **Configuratie maakt deel uit van het programma.** Dezelfde binary, andere GC-modus/cores/limieten ⇒ ander geheugengebruik en andere latency.
2. Server-GC ruilt geheugen in voor doorvoer; een kleine service op een grote node heeft zelden 20 heaps nodig.
3. In containers leest de runtime de cgroup-geheugen-/CPU-limieten; weet wat hij leest en wat de standaardwaarden zijn (DATAS op .NET 9+ helpt).
4. Print altijd GC-modus, core-aantal en limieten bij het begin van elke benchmark of incidentrapport (deze harness doet dat).

## Extra credit
Met DATAS weer aan gaan de gen0-collecties van 15 naar 1.880 per run, maar de looptijd beweegt nauwelijks. Waarom zijn die collecties hier zo goedkoop? Hoe zou een workload eruitzien die er *wel* voor betaalt?

## Go further
Draai de exercise met `DOTNET_gcServer=0` en met `DOTNET_GCHeapCount=1..8` en zet committed memory en tijd tegen elkaar uit. Waar ligt jouw knikpunt?
