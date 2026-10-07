# L02-01 - Oplossing

## Wat het profiel laat zien

- **allocaties:** verwacht dat `System.Int32` (boxed) het meest voorkomende type is qua objectaantal: ongeveer drie boxes per
  meting, 24 bytes elk, plus de `object[]`-arrays waar `ArrayList` en `Hashtable` in groeien.
- **tracing:** tijd is verspreid over `ArrayList.Add`, `Hashtable.set_Item`/`get_Item`, en `ArrayList.Sort` (via
  `Comparer.Default` en `IComparable.CompareTo`). Niets is op zichzelf "traag".

## Grondoorzaak
`ArrayList` en `Hashtable` zijn non-generiek: ze slaan `object` op. Elke `Add(int)` **boxt** (alloceert een heap-object per waarde);
elke `(int)`-cast **unboxt**. Het histogram boxt twee keer per meting: één keer voor de key bij het opzoeken en één keer voor de waarde bij het opslaan.
Dat is ruwweg drie allocaties per meting. Het sorteren van een `ArrayList` vergelijkt via `IComparable` op boxed waarden in plaats van `int`s direct te vergelijken.

## Fix
Gebruik de generieke collecties: `List<int>`, `Dictionary<int,int>`, en `List<int>.Sort()`. De waarden blijven unboxed binnen de arrays.

## Lessen
1. **Boxing toont zich als allocaties van een primitief type.** Als het meest voorkomende type in het geheugen `System.Int32` of `System.Double` is, zoek dan naar `object`, non-generieke collecties, of een interface-call op een ongeconstrainede struct.
2. De resterende 4 MB komt van `List<int>` die groeit door te verdubbelen (ongeveer 2x de uiteindelijke grootte) plus de dictionary. `new List<int>(capacity)` haalt het grootste deel daarvan weg.
3. Boxing verstopt zich ook in `string.Format("{0}", 5)`, `params object[]`, `IComparable` (non-generiek), en `struct`-waarden gebruikt via `object`-getypeerde API's. Interpolated strings vermijden veel hiervan in moderne C#.

## Ga verder
Vervang de dictionary door een `int[]`-histogram, `CollectionsMarshal.GetValueRefOrAddDefault`, of één pass die som/max berekent tijdens het toevoegen.
Schrijf daarna een geval waarin de generieke versie *niet* sneller is (hint: heel kleine N).