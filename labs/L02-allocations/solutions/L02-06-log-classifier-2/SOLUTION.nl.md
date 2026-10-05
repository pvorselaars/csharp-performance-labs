# L02-06 - Oplossing

## Wat het profiel laat zien
- **allocaties:** `Match`, `GroupCollection`, `Group[]`, `Capture`-arrays en veel `string` (elke `.Value`, inclusief de drie die je niet in leven hoeft te houden).
- **tracing:** de compiled regex-runner is nu goedkoop. Het grootste deel van de resterende tijd is allocatie- en GC-gerelateerd.

## Grondoorzaak
Reguliere expressies zijn uitstekend voor flexibel matchen en slecht voor het parsen van grote hoeveelheden in een vast, eenvoudig formaat: elke `Match` produceert een graaf van objecten, en `.Value` produceert strings. De kosten zijn verschoven van het *construeren* van de regex (L1) naar het *consumeren van de resultaten*.

## Fix
Parse de vaste grammatica handmatig over een `ReadOnlySpan<char>`: zoek de eerste spatie (tijdstempel), lees het hoofdletterniveau tot de volgende spatie, lees de service tot `" - "`, en controleer of de rest eindigt met `" status=ddd"`.
Alloceer alleen de twee strings en de `LogEntry` die deel uitmaken van het resultaat (of vermijd ook die: zie Extra credit).
Houd de grammatica identiek. In het bijzonder betekent de lazy `.*?` van de regex gevolgd door een optionele groep bij `$`: "het bericht eindigt waar een afsluitende ` status=ddd` begint, als die er is".

## Lessen
1. **Het fixen van één kostenpost onthult vaak de volgende.** Regex-constructie verborg regex-*resultaten*. Profileer opnieuw na elke fix.
2. Een handgeschreven parser is sneller maar is code die je nu moet onderhouden en testen. Dat is de juiste keuze voor een hot path met een stabiel formaat, de verkeerde voor een flexibel formaat. De checksum doet hier het werk dat een testsuite zou doen.
3. De resterende 10,9 MB is geen verspilling in de parser; het zijn de *resultaatobjecten*. Of dat ertoe doet hangt af van wat consumenten nodig hebben. Het resultaattype veranderen is een API-beslissing, geen micro-optimalisatie.
4. `Regex.EnumerateMatches` en `IsMatch` vermijden `Match`-objecten, maar geven je geen groepswaarden, dus ze kunnen deze parser niet direct vervangen.

## Ga verder
Retourneer een `readonly record struct` en kijk of de aanroepende lus alle allocatie kan vermijden. Benchmark daarna tegen `[GeneratedRegex]` met `EnumerateMatches` voor één groep.