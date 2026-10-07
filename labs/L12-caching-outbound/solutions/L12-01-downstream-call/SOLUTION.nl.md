# L12-01 - Oplossing

## Grondoorzaak
Een nieuwe `HttpClient`/handler (en connection pool) per request verhindert hergebruik van verbindingen.

## Fix
`IHttpClientFactory` (`AddHttpClient`) en `factory.CreateClient()`: handlers worden gepoold en volgens een schema geroteerd. (Een singleton `HttpClient` met `PooledConnectionLifetime` is het alternatief.)

## Lessen
1. **Hergebruik de handler, niet de request.** Dit is L05-04 op het requestpad, waar load het vermenigvuldigt.
2. Door de factory beheerde clients voorkomen zowel socketuitputting als verouderde DNS.
3. Typed clients + resilience-handlers (timeouts, retries) hangen van nature aan de factory.

## Extra credit
Gebruik een `static readonly HttpClient` zonder `PooledConnectionLifetime`. Wat breekt er als de DNS van de callee verandert?

## Ga verder
Voeg een typed client en een `Microsoft.Extensions.Http.Resilience`-pipeline toe. Wat verandert er in `connections`?
