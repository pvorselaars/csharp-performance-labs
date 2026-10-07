# L07-01 - Oplossing

## Wat het profiel laat zien

- **Timeline:** threads geblokkeerd op één monitor en op `Task.Wait`; het aantal threads kruipt omhoog; CPU bijna op nul; de database ziet steeds maar één call tegelijk.
- Na het verwijderen van de lock: veel identieke databasecalls voor dezelfde SKU (stampede). Daarna: 20 sequentiële round trips per request.

## Grondoorzaak
Vier gestapelde defecten. (1) De cache laadt **terwijl een lock wordt vastgehouden**, dus elke miss in het proces serialiseert. (2) Hij laadt met **sync-over-async** (`.Result`) op een pool-thread, waardoor geblokkeerde threads de pool uithongeren. (3) Zodra de lock weg is, raken concurrent misses voor *dezelfde* SKU allemaal de database (**stampede**). (4) Een request awaitet zijn 20 prijzen **één voor één** in plaats van concurrent, en bouwt het bonnetje op met string-concatenatie.

## Fix
`ConcurrentDictionary<int, Lazy<Task<int>>>` zodat elke SKU één keer wordt geladen en gedeeld, geawait (geen blocking, geen lock); zoek de 20 prijzen van een request op met `Task.WhenAll`; bouw het bonnetje met een `StringBuilder`. Elke stap is een fix uit Lab 4 (en de stampede-les uit L12).

## Lessen
1. **Echte incidenten stapelen zich op.** De eerste fix maakt het werk zelden af; hij verplaatst het knelpunt. Meet opnieuw na elke verandering.
2. Elke laag had een eigen signatuur (geblokkeerd-op-monitor, starvation, dubbele calls, sequentieel wachten); ze snel herkennen is de opbrengst van de eerdere labs.
3. Caching verbergt load totdat het niet meer lukt: een lege cache onder een burst is precies het moment waarop misses en stampedes pijn doen.
4. **Warm-up verbergt opstartbugs.** Thread-pool-starvation is het ergst op een vers proces, en dat is precies lanceerdag of net na een deploy. Meet de eerste request na de start, niet alleen de steady state.
5. Schrijf de post-mortem: welk defect verborg welk ander? Die volgorde is de les.

## Extra credit
Wat gebeurt er als de databasecall voor één SKU *faalt*? Blijft een gefaalde `Lazy<Task>` in jouw fix voor altijd gecachet? Ontwerp het gedrag dat je wilt.

## Go further
Voeg een begrensde mate van parallellisme per request toe (bijv. `SemaphoreSlim(8)`) en een globale limiet voor de database, en leg uit wanneer je elk nodig hebt.
