# L01-03 - Oplossing

## Wat het profiel laat zien
Onder `LineParser.Parse` wordt de tijd gedomineerd door het **opbouwen** van de regex (patroon parsen,
codegeneratie, char-class-opzet, constructor-frames) in plaats van door het **matchen** (interpreter/scan-frames).
Het allocatieprofiel toont ~390 MB aan wegwerpobjecten, vooral afkomstig van dat opbouwwerk.

## Grondoorzaak
`new Regex(pattern)` draait bij elke aanroep. Het patroon parsen en het matching-programma bouwen is veel
duurder dan het matchen van één korte regel, en het patroon verandert nooit.

## Fix
Bouw hem één keer: `static readonly Regex`. (Regex-instanties zijn thread-safe om mee te matchen, delen is dus prima.)

| variant | tijd | toegewezen |
|---|---|---|
| vooraf (`new Regex` per aanroep) | ≈ 690–735 ms | 377,6 MB |
| static readonly, interpreted | ≈ 153 ms | 45,2 MB |
| static `Regex.Match(line, pattern)` (interne cache) | ≈ 154 ms | 45,2 MB |
| `[GeneratedRegex]` (source-generated) | ≈ 122 ms | 45,2 MB |
| static readonly + `RegexOptions.Compiled` (**meegeleverd in de oplossing**) | ≈ 65 ms | 45,2 MB |

Resultaten zijn patroon- en runtime-afhankelijk: `Compiled` won hier; neem niet aan dat dat altijd zo is. Meet je eigen situatie.

## Take-aways
1. **Warm-up verbergt eenmalige kosten.** `RegexOptions.Compiled` en `[GeneratedRegex]` ruilen opstartkosten in
   voor doorvoer. In een langlopende service is dat een koopje; in een kortlevende CLI kan het verlies zijn.
   `[GeneratedRegex]` verplaatst de kosten bovendien naar build-tijd en is trimming/AOT-vriendelijk.
2. De static `Regex.Match(input, pattern)`-overload werkt dankzij een interne cache, maar die cache is klein
   (standaard 15 entries), voegt een lookup per aanroep toe, en valt stilletjes van een klif als je veel patronen gebruikt.
3. Drie varianten, drie verschillende getallen: dit is precies waarom het budget een *gate* is, geen scoreboard.

## Extra credit
Probeer na het slagen drie verschillende fixes en vergelijk ze.
Ook: wat alloceert er na jouw fix nog steeds, en waarom?

## Verder
De overgebleven 45 MB zijn `Match`/`Group`-objecten en de strings die `.Value` aanmaakt, plus de testdata zelf.
Dat is een Lab 2-exercise: parse met `IsMatch`/`EnumerateMatches`, `ValueMatch`, of handgeschreven
`Span<char>`-parsing, en kijk hoe dicht je bij nul allocaties uitkomt.
