# L06-08 - Oplossing

## Wat het profiel laat zien

- **Sampling:** alle tijd in LINQ's `Count` en de lambda, één element tegelijk.

## Grondoorzaak
Een scalaire loop met één delegate-call per element, terwijl er een gevectoriseerde BCL-primitive bestaat.

## Fix
`((ReadOnlySpan<int>)Data).Count(42)` (`MemoryExtensions.Count`), gevectoriseerd en allocatievrij. Past er geen primitive, dan zijn `Vector<T>`/`Vector128<T>` de volgende tool, na het meten.

## Lessen
1. **Zoek eerst naar een gevectoriseerde BCL-primitive**: `Count`, `IndexOf`, `Contains`, `SequenceEqual`, `Sum` (op spans), `Max`... zijn handmatig getuned.
2. LINQ over arrays is handig, maar delegate-calls per element verhinderen inlining en vectorisatie; gebruik in hot paths span-methodes of loops.
4. Controleer wat je kreeg: de disassembly (`vpcmpeqd`/`vpaddd`) is het bewijs van vectorisatie.

## Extra credit
Draai de fix met `DOTNET_EnableAVX2=0` (128-bit SSE, 4 ints per vergelijking) en met `DOTNET_EnableHWIntrinsic=0` (geen SIMD). Wat zegt dat over deployen naar oudere of kleinere hardware? Maak daarna, in een scratch-kopie, `Data` 64 keer groter en `Passes` 64 keer kleiner (hetzelfde aantal vergelijkingen) en draai alle drie opnieuw. Waarom komen ze nu zoveel dichter bij elkaar uit?

## Ga verder
Schrijf de loop handmatig met `Vector<int>` en vergelijk met `Count`. Vergelijk daarna met een gewone `for`-loop over de span (vectoriseert de JIT die? (Nee; hij auto-vectoriseert niet.)).
