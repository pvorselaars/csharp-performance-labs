# L10-04 - Oplossing

## Wat het profiel laat zien

- **Counters:** gezonde pool, lage CPU. **Uitgaande calls:** ≈ één per request; **queue:** requests die wachten op `SemaphoreSlim`.

## Grondoorzaak
Een globale `SemaphoreSlim(1)` serialiseert een trage, cachebare externe call voor elke request, waardoor de doorvoer begrensd wordt tot één calltijd en elke request de wachtrij betaalt.

## Fix
Cache de fetch per valuta als een gedeelde `Lazy<Task<decimal>>` (één call per key, geawait door allemaal). In productie voeg je expiry, refresh-ahead en foutafhandeling toe (`HybridCache` doet veel hiervan al).

## Lessen
1. **Een async lock verwijdert thread-blocking, maar niet serialisatie.** Een poort op het hot path begrenst de doorvoer.
2. Als de data gedeeld is en traag verandert, **cache hem** en dedupliceer gelijktijdige fetches (single-flight).
3. Bepaal wat er gebeurt bij falen en bij verlopen: een gecachte gefaalde task mag niet voor altijd gecached blijven.
4. Lock *vernieuwen*, niet *lezen*.

## Extra credit
Wat als de koers per *gebruiker* kan verschillen (veel keys)? Wat zou je gebruiken om de cachegrootte te begrenzen?

## Verder
Voeg een expiry van 5 seconden toe met refresh-ahead: serveer de verouderde waarde terwijl één request vernieuwt. Wat kan er misgaan als die vernieuwing faalt?
