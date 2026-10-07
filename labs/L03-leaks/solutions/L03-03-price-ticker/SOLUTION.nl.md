# L03-03 - Oplossing

## Wat het profiel laat zien
> Illustratief: de profiler-views zijn wat de code impliceert (geen profiler-opname).

- **snapshots:** +1.000 `Ticker`, +1.000 `byte[]` (20.000). Retentiepad: GC-root (timer queue / `TimerQueue`) → `TimerQueueTimer` → callback-delegate → closure (`<>c__DisplayClass`) → `Ticker`.
- **timeline:** thread-pool-threads worden eens per seconde per gelekte timer wakker.

## Grondoorzaak
Elke `Ticker` start een periodieke `Timer` waarvan de callback `this` vastlegt. Timers worden door de runtime geroot, niet door jouw variabelen, dus een niet-gedisposede timer houdt zijn callback en de `Ticker` (met zijn 20 KB-cache) in leven, en blijft afgaan.

## Fix
Maak `Ticker` `IDisposable`, dispose de timer, en gebruik `using` op de ticker. Alles wat achtergrondactiviteit start (timers, `CancellationTokenSource`s met registraties, event-abonnementen, tasks) heeft een bijbehorende *stop* nodig.

## Take-aways
1. **Achtergrondactiviteit is een root.** Timers, lopende tasks, registraties en event-abonnementen houden hun targets in leven zonder dat enige variabele van jou ernaar wijst.
2. Het retentiepad laat zien *wat* het vasthoudt; de fix is om te vinden wat het *start* en de bijbehorende stop toe te voegen.
3. Analyzers helpen: CA2000 (dispose objecten voordat ze uit scope gaan) en IDE0067/CA1001 (types die disposable fields bezitten, moeten zelf disposable zijn).
4. Als de callback `this` vastlegt, overweeg dan een static callback met expliciet doorgegeven state, zodat de timer de eigenaar niet root (en dispose nog steeds).

## Extra credit
Maak de `Timer` met `dueTime: Timeout.Infinite` in een scratch-kopie. Is het nog steeds een lek? Waarom wel of niet?

## Go further
Zorg dat de callback de ticker niet root (weak reference, of een `static` lambda + state). Lost dat het lek op zonder `Dispose`? Wat is er dan nog steeds mis?
