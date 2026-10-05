# L11-01 - Oplossing

## Grondoorzaak
Een per-klant query binnen een loop: N+1 op het request-pad, waarbij concurrency het databasewerk vermenigvuldigt met de fan-out.

## Fix
Eén projectie met een server-side aggregaat (`Select(c => c.Orders.Sum(...))`), `AsNoTracking`.

## Lessen
1. **Statements per request** is de metric die N+1 in productie blootlegt; zet er een alert op.
2. Concurrency maakt van een verspillend querypatroon databaseverzadiging.
3. Vind het via traces voordat je code leest (het checkpoint van de ASP.NET Core-labs (9-14)).

## Extra credit
Laad in plaats daarvan de *orders zelf* met `Include`. Hoe verhoudt het rijenaantal en de allocatie zich tot de projectie?

## Verder gaan
Voeg een OpenTelemetry/EF logging-listener toe die statements per request telt en een test laat falen boven de 3.
