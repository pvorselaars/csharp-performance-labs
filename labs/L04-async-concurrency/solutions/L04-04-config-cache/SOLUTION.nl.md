# L04-04 - Oplossing

## Wat het profiel laat zien

- **Metric:** `factoryCalls` ≈ 20 x (tot 8) in de trage versie; precies 20 in de fix.
- **CPU-tijd:** ~8x de fix, omdat elke ronde z'n 8 threads allemaal 5 ms verbranden.

## Grondoorzaak
`ConcurrentDictionary.GetOrAdd(key, Func)` locked niet tijdens het draaien van de factory. Threads die gelijktijdig missen, draaien hem elk afzonderlijk; alleen het eerste resultaat wordt opgeslagen. Voor dure of side-effecting factories is dat overbodig werk (en, met side effects, mogelijk zelfs fout gedrag).

## Fix
Sla `Lazy<T>`-waarden op: `GetOrAdd(key, k => new Lazy<T>(() => Load(k))).Value`. Meerdere threads kunnen een `Lazy` aanmaken (goedkoop), maar de standaard thread-safety-modus garandeert dat de loader maar één keer draait. (Async-versie: cache een `Lazy<Task<T>>` of gebruik `HybridCache`.)

## Lessen
1. **`GetOrAdd` is geen 'bereken één keer'.** Alleen de *opgeslagen waarde* is uniek.
2. Verspild werk uit zich vaak in CPU en belasting van dependencies, niet in latency; meet allebei.
3. `Lazy<T>`-modi: `ExecutionAndPublication` (standaard, draait één keer), `PublicationOnly` (kan vaker draaien), `None` (niet thread-safe).
4. Dit is het kiemgetal van een **cache stampede** (Lab 12): veel misses berekenen tegelijk dezelfde dure waarde opnieuw.

## Extra credit
Print `factoryCalls` voor 2, 8 en 32 threads. Wat doet dat getal, en waarom is het niet altijd 20 × threads?

## Go further
Maak de loader `async` en cache `Lazy<Task<int>>`. Wat gebeurt er als de task faalt: wordt de fout voor altijd gecachet?
