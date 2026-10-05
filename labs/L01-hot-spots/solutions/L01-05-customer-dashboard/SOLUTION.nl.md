# L01-05 - Oplossing

## Wat het profiel laat zien
- **Sampling:** tijd zit in `ComputeScore` (de `Math.Sqrt`-loop). Volkomen redelijk frame, volkomen redelijke
  call stack; niets lijkt kapot. Daarom onthult sampling alleen dit niet.
- **Tracing:** `ComputeScore` wordt ruwweg **3x per actieve klant** aangeroepen (plus 1 voor `Any`). Het
  *aantal aanroepen* is de aanwijzing.

## Grondoorzaak
`scored` is een **uitgestelde query**, geen collectie. Elke terminale operatie draait de hele
`Where → Select`-pipeline opnieuw, inclusief de dure selector: `Count()` (ja, zelfs `Count`: de selector
draait om neveneffecten te behouden), `Sum(...)`, en `OrderByDescending(...).First()` zijn drie volledige
doorlopen, en `Any()` voegt nog een evaluatie toe.

## Fix
Eén keer materialiseren: `.ToList()`, en gebruik dan `Count` (de property), `Sum`, en het sorteren op de lijst.

De versnelling van ~2,9x komt bijna precies overeen met de ~3 overbodige evaluaties; die rekensom is een goede
sanity check dat je het *hele* probleem gevonden hebt.

## Take-aways
1. **Kies de profiler-modus bij de vraag.** "Waar gaat tijd heen?" → sampling. "Hoe vaak draaide dit?" →
   tracing. Tracing voegt overhead toe en vertekent absolute tijden, gebruik het dus voor aantallen en bevestig
   daarna met sampling.
2. Statische analyse helpt: Rider/ReSharper's *Possible multiple enumeration* en analyzer CA1851 signaleren dit.
3. `ToList()` is niet gratis (allocatie, geheugen vastgehouden). Voor één doorloop kun je het helemaal
   overslaan en count/sum/top in één `foreach` berekenen.

## Verder
Schrijf de single-pass-versie en vergelijk. Check dan: klopt de checksum nog? (De optelvolgorde van
floating-point getallen doet ertoe: hou de iteratievolgorde gelijk.)
