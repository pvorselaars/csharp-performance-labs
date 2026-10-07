# L11-boss - Oplossing

## Grondoorzaak
Eén defect uit elk van drie Lab 11-exercises.

## Fix
Doe de trage externe call voordat je een connection leent; `AsSplitQuery()` voor de twee collecties; bereken het totaal uit de geladen orders in plaats van per order te queryen.

## Lessen
1. **Defect -> bron:** connection vastgehouden tijdens een trage call = **L11-03**; twee collectie-`Include`s = **L11-02**; een query per order = **L11-01**.
2. Elk heeft zijn eigen metric: pool-wachtenden, teruggegeven rijen, statements per request. Welke bekeek jij als eerste?
3. Het fixen van de N+1 en de explosie helpt de p99 nauwelijks totdat de vasthoudtijd van de pool is gefixt: de doorvoer wordt begrensd door grootte ÷ vasthoudtijd.

## Extra credit
Welke ene wijziging verplaatst de p99 het meest? `sqlCommands` het meest?

## Verder gaan
Projecteer direct naar een DTO met een server-side aggregaat en vergelijk met split queries.
