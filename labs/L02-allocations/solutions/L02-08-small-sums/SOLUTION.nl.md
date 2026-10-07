# L02-08 - Oplossing

## Wat het profiel laat zien
- **Allocaties:** geboxte `List<int>+Enumerator`, ~2.000.000 instanties (één per call).

## Grondoorzaak
Boxing van de struct-enumerator van `List<T>` wanneer een lijst wordt doorlopen via `IEnumerable<T>`, plus interface-calls per element.

## Fix
Accepteer `ReadOnlySpan<int>` (of `List<int>`/`int[]` wanneer je het concrete type nodig hebt). Geen enumerator-object, geen dispatch, en de JIT kan unrollen/vectoriseren.

## Lessen
1. **`IEnumerable<T>`-parameters zijn handig en kosten een geboxte enumerator per call** op hot paths.
2. Spans (of concrete collectietypes) vermijden zowel boxing als virtuele calls.
3. Publieke API's die 'alles' moeten accepteren, kunnen overloads bieden: `ReadOnlySpan<T>` voor het hot path, `IEnumerable<T>` voor het algemene geval.
4. LINQ over `IEnumerable<T>` heeft hetzelfde kostenprofiel (L02-02).

## Extra credit
Wat gebeurt er als je `Values.AsEnumerable()` doorgeeft versus `(IReadOnlyList<int>)Values`? Voorspel de allocaties.

## Ga verder
Voeg overloads toe voor `int[]` en `IEnumerable<int>`; controleer aan welke elke call site bindt.