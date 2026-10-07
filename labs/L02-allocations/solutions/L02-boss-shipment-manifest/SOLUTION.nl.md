# L02-boss - Oplossing

## Wat het profiel laat zien
- Meerdere onafhankelijke kostenposten: string-opbouw door concatenatie, een lineaire zoekactie met een closure per regel, `Split`/`Trim`/`ToUpper`-rommel, en een `ArrayList` van geboxte doubles.

## Grondoorzaak
Vier defecten uit eerdere labs opgestapeld in één methode.

## Fix
`StringBuilder` voor het manifest; een `Dictionary` met de carrier-code als key; `List<double>`; span-slicing plus een stackbuffer voor de uppercase-code in plaats van `Split`/`Trim`/`ToUpperInvariant`.

## Lessen
1. **Defect -> bron:** `manifest +=` in een lus = **L01-01**; `Carriers.FirstOrDefault(...)` per regel = **L01-02** (en de closure die hij alloceert = **L02-02**); `Split`/`Trim`/`ToUpperInvariant`-rommel = **L02-02**; `ArrayList` van `double` = **L02-01** (boxing).
2. Heb je alle vier gevonden? Welke vond je *als laatste*, en waarom?
3. Het eerst fixen van de grootste verandert het profiel: de resterende kosten worden pas zichtbaar nadat die weg is.

## Extra credit
Welke van de vier fixes leverde de meeste tijdswinst op? De meeste allocatiewinst? Zijn dat dezelfde?

## Ga verder
Herschrijf zodat het parsen van de regel een `readonly record struct` oplevert in plaats van losse delen. Waar komt de resterende allocatie vandaan?