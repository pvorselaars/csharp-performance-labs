# L01-boss - Oplossing

## Wat het profiel laat zien
> Illustratief: profiler-weergaven zijn wat de code impliceert (geen daadwerkelijke profiler-opname).

- Vijf onafhankelijke Lab 1-defecten: een `Regex` gebouwd per regel, exceptions voor foute bedragen, een lazy
  query die vier keer geënumereerd wordt (de parse elke keer opnieuw uitvoerend), een lineaire klantlookup per
  rij, en kwadratische stringopbouw.

## Grondoorzaak
Eén defect uit elk van de vijf Lab 1-exercises.

## Fix
Static `Regex`; `decimal.TryParse`; één keer `ToList()`; `Dictionary`-lookup; `StringBuilder`.

## Take-aways
1. **Defect > bron:** `new Regex` per regel = **L01-03**; `try/catch` rond `Parse` = **L01-04**; een lazy
   `Select/Where` geënumereerd door `Any`/`Count`/`Sum`/`foreach` = **L01-05**; `FirstOrDefault` per rij =
   **L01-02**; `report +=` in een loop = **L01-01**.
2. Welke vond je als eerste, en welke als laatste? De grootste fixen verandert wat het profiel daarna laat zien.
3. Heb je na elke stap je fix tegen de checksum gecontroleerd?

## Extra credit
Welke ene fix haalt het meeste tijd weg? Welke haalt de meeste allocatie weg?

## Verder
Voeg zelf een zesde defect toe uit L01-06/L01-07 en kijk of een collega het vindt.
