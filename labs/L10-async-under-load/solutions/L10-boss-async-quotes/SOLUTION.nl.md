# L10-boss - Oplossing

## Grondoorzaak
Eén defect uit drie exercises van Lab 10.

## Fix
`await` overal (geen geblokkeerde pool-threads), en één `SemaphoreSlim` gedeeld door alle requests voor de downstream.

## Lessen
1. **Defect -> bron:** `Thread.Sleep` in een `async`-handler = **L10-02**; `.Result` = **L10-01**; onbegrensde fan-out x concurrency = **L10-03**.
2. Blokkerende defecten hongeren de pool uit (zichtbaar in pool-counters); de fan-out overbelast de downstream (zichtbaar als in-flight count): verschillende tools vonden verschillende defecten.
3. Welke heb je als eerste gefixt, en hoe zag het volgende profiel eruit?

## Extra credit
Welke fix beweegt de p99 het meest? En `peakInflight`?

## Verder
Voeg een request-deadline toe (`CancellationToken`) en geef 503 als de wait op een permit die overschrijdt (L10-05).
