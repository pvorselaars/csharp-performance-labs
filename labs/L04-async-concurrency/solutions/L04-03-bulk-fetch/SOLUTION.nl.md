# L04-03 - Oplossing

## Wat het profiel laat zien

- **Metric:** `peakInflight` ≈ 1.000 in de trage versie, 50 in de fix.
- **Timeline:** duizenden continuations die in een burst afronden; de latency per call stijgt mee met het aantal in flight.

## Grondoorzaak
Onbegrensde fan-out. De call van elk item wordt meteen gestart, dus de downstream ziet 1.000 gelijktijdige requests en (in dit model) reageert traag op allemaal. Echte systemen kunnen timeouts geven, load afstoten of omvallen, wat retries triggert en het erger maakt.

## Fix
Begrens de concurrency: `Parallel.ForEachAsync` met `MaxDegreeOfParallelism = 50` (of een `SemaphoreSlim`, of een `Channel` met N consumers). Kies de limiet op basis van de capaciteit van de downstream, niet op basis van je eigen aantal cores.

## Lessen
1. **Onbegrensd parallellisme is niet gratis**: het verplaatst de queue van jouw code naar de downstream (en naar het geheugen).
2. Kies de limiet op basis van meting: verhoog hem tot de doorvoer stopt met verbeteren of de latency begint te stijgen.
3. `Task.WhenAll` over een grote sequence is een geurtje; `Task.Run` in een loop ook. Gebruik een begrensde primitief.
4. Begrensde concurrency begrenst ook het geheugen (minder levende tasks en buffers).

## Extra credit
Vervang `Parallel.ForEachAsync` door een `SemaphoreSlim`-gated `Task.WhenAll`. Zelfde gedrag? Wat zijn de afwegingen?

## Go further
Veeg `MaxDegreeOfParallelism` van 5 tot 500 en plot de totale tijd. Waar zit de knik? Wat zou een andere downstream (`n*n/500`) veranderen?
