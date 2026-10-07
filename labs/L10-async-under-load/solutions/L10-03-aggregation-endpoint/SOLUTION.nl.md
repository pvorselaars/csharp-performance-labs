# L10-03 - Oplossing

## Wat het profiel laat zien
- **Metric:** `peakInflight` ≈ 480 (traag) vs ≤ 40 (fix). **Downstream-latency:** loopt scherp op met het aantal tegelijk onderweg.

## Grondoorzaak
Onbegrensde fan-out vermenigvuldigd met gelijktijdige requests overbelast de gedeelde downstream, wat elke call vertraagt: latency wordt bepaald door het *product* van de twee concurrency-niveaus.

## Fix
Een `SemaphoreSlim(40)`, gedeeld over alle requests, reguleert calls naar de downstream. Requests wachten kort in de rij in plaats van hem te overspoelen.

## Lessen
1. **Begrens concurrency bij de gedeelde resource**, niet per request: totaal onderweg = som over alle requests.
2. Dit is L04-03 op request-schaal; dezelfde 'sweet spot'-logica.
3. Een bulkhead per afhankelijkheid voorkomt ook dat één trage afhankelijkheid al je capaciteit opsoupeert.
4. Overweeg ook een timeout en load shedding: eindeloos wachten is zelf ook een faalmodus.

## Extra credit
Voeg een timeout van 100 ms toe aan `WaitAsync` en geef 503 terug als die verloopt. Wat doet dat met de p99 en met het foutpercentage?

## Verder
Varieer het aantal permits (5 tot 200) en plot de p99. Vervang daarna de semafoor door `Parallel.ForEachAsync` per request en leg uit waarom dat het totaal niet fixt.
