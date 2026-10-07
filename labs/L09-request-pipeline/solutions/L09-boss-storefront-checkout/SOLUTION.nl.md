# L09-boss - Oplossing

## Wat het profiel laat zien
- Vier onafhankelijke L9-defecten op één request-pad.

## Grondoorzaak
Eén defect uit elk van vier Lab 9-exercises.

## Fix
Static `Regex` en `[LoggerMessage]` in de middleware; de belastingtabel als echte singleton; deserialiseren rechtstreeks vanuit de body-stream; de bon één keer bouwen en één keer schrijven.

## Lessen
1. **Defect -> bron:** `Regex` per request en geïnterpoleerd loggen = **L09-02**; `Transient` per request = **L09-04**; body -> string = **L09-05**; flushen per regel = **L09-03**.
2. Elke laag van de pipeline had zijn eigen signatuur in de allocatieweergave: welke types wezen naar welke?
3. Meet bytes per request voor en na elke fix: welke gaf de grootste daling?

## Extra credit
Welk enkel defect veroorzaakt de gen2-collecties?

## Verder graven
Vergelijk het resultaat met de bodem van L09-01: hoeveel bytes per request blijven er over boven een leeg endpoint?
