# L12-02 - Oplossing

## Wat het profiel laat zien
> Illustratief: profiler-views zijn wat de code impliceert (geen profiler-opname).

Gemeten op de machine van de auteur (ongeschaald):

| | Traag | Gefixt |
|---|---|---|
| Mediane tijd | ≈ 252 ms | ≈ 51 ms |
| Mediane p99 | ≈ 251 ms | ≈ 51 ms |
| `loads` | 40 | 5 |

- **Metric:** `loads` 40 (traag) vs precies 5 (fix): tientallen in een echt systeem, hier precies 40 omdat alle 200 requests binnen het koude-cache-venster vallen.

## Grondoorzaak
Niet-atomaire get-or-create in een hot cache: elke gelijktijdige miss herberekent dezelfde waarde (thundering herd). De backend achter de loader kan maar 8 calls tegelijk bedienen, dus 40 overbodige loads wachten in golven van 8 - zo'n 5 golven x 50 ms ≈ 250 ms - terwijl 5 echte loads in één golf passen en in ≈ 50 ms klaar zijn. Het verspilde werk is niet alleen extra backend-load: het verhoogt direct de latency die elke aanroeper ziet, ook degenen wier key nooit overbodig was.

## Fix
Single-flight per key met `Lazy<Task<T>>` in een `ConcurrentDictionary` (zoals hier), of `HybridCache.GetOrCreateAsync`. Voeg expiry toe en bepaal wat er gebeurt bij falen (cache geen faulted task voor altijd).

## Lessen
1. **Een cache miss onder load is een gesynchroniseerde gebeurtenis.** Warm caches op voordat je verkeer accepteert, en bescherm de loader.
2. Dit is de kiem van veel incidenten: koude cache + burst → backend-overload → timeouts → retries → meer load.
3. `HybridCache` geeft stampede-bescherming en L1+L2-caching; verkies het boven handgerolde code.

## Extra credit
Voeg een expiry van 100 ms toe en draai opnieuw. Wat doet de *periodieke* herlaad met p99? (Dit is de kiem van L14-04.)

## Ga verder
Probeer `HybridCache` (heeft het `Microsoft.Extensions.Caching.Hybrid`-package nodig) en vergelijk `loads`.
