# L07-04 - Oplossing

## Wat het profiel laat zien

- **Sampling:** bijna de hele tijd van `Workload.Run` is de eigen tijd van `Dictionary.FindValue`. Niets van jouw eigen code is zichtbaar.
- **Tracing:** hetzelfde beeld. `FindValue` heeft 92% van de tijd als *eigen* tijd; `IsValid` en `Key.GetHashCode` tellen samen op tot minder dan een milliseconde voor de hele sessie. `Key.Equals` verschijnt helemaal niet onder `FindValue`.

## Grondoorzaak
Een zwakke `GetHashCode` (`A + B`) geeft veel verschillende keys dezelfde hash, waardoor elke dictionary-lookup een lange keten van `Equals`-calls afloopt. De profielen misleidden niet, maar stopten één niveau te vroeg: het hete frame is de code van het framework, `FindValue`, en de oorzaak is de hashcode die jouw type eraan meegeeft. `GetHashCode` zelf is goedkoop en oogt onschuldig in elk profiel.

## Fix
Gebruik een hash die beide velden mixt: `HashCode.Combine(A, B)`. Laat `IsValid` ongemoeid.

## Lessen
1. **Een profiel laat zien waar de tijd zit, niet wiens schuld het is.** Als het hete frame framework-code is (`FindValue`, `Sort`, `Concat`), vraag je af wat *jouw* code eraan meegeeft: keys, comparers, groottes.
2. **Geïnlinede methoden zijn onzichtbaar**, zowel in sampling als tracing: hun kosten worden geboekt bij de caller. Een piepklein methodetje dat "niets kost" in het profiel kan alsnog de oorzaak zijn.
3. Slechte hashcodes zijn een stille kwadratische kostenpost: controleer de verdeling voor elk type dat als key wordt gebruikt (tel botsingen).
4. De fix is één regel; uitzoeken *welke* regel vergde de juiste tool-interpretatie.

## Extra credit
Zet `[MethodImpl(MethodImplOptions.NoInlining)]` op `Key.Equals` in een scratch-kopie en profileer opnieuw, in beide modi. Waar verplaatst de tijd naartoe, en hoe verandert de looptijd? Wat zegt dat over het vertrouwen op een profiel van code die de JIT kan inlinen?

## Go further
Tel de distincte hashcodes die jouw keys opleveren (een `HashSet<int>` van `GetHashCode()`-resultaten) voor `A + B`, `HashCode.Combine(A, B)` en `(A * 397) ^ B`. Welke is goed genoeg voor deze keys, en waarom is `HashCode.Combine` de veilige standaardkeuze?
