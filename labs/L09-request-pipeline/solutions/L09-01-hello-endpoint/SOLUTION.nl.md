# L09-01 - Oplossing

## Wat het profiel laat zien
- **Allocatie per type:** controller-activatie, `ActionContext`/filter-pipeline-objecten, formatter- en result-objecten per request in de MVC-versie; een handjevol in het minimale endpoint.

## Grondoorzaak
Pipeline-features die je niet gebruikt, draaien toch: MVC's controller-activatie, filter-pipeline en JSON-output-formatting zijn werk per request met allocatie per request.

## Fix
Een minimaal endpoint dat een vast result teruggeeft (hier `Results.Text`). Zie dit niet als 'gebruik nooit MVC': gebruik het om te **kalibreren** wat een request kost, en beoordeel daarna elk echt endpoint tegen die bodem.

## Lessen
1. **Ken de bodem.** Meet eerst het lege endpoint; elke extra byte per request daarboven komt van jouw code of een framework-feature die je zelf koos.
2. Frameworks hebben een belasting per request, evenredig met de features in de pipeline; het is een begrotingspost, geen bug.
3. Meet per request (totaal ÷ requests), niet per run.
4. Een harness die de client meerekent, onderschat het aandeel van de server: houd daar rekening mee bij het lezen van de getallen.

## Extra credit
Geef het object terug vanuit een minimale API (`Results.Ok(new { message = "hello" })`) in plaats van een kant-en-klare string. Waar komt het verschil vandaan?

## Verder graven
Voeg response-compressie, CORS, auth (een dummy-scheme) en logging-middleware één voor één toe. Wat voegt elk toe per request?
