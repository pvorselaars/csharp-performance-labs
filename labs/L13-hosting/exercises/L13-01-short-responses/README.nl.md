# L13-01 - Short responses

*Short and Not So Sweet*

## Symptoom
**Elk request kost een verse TCP-connectie** (en, in productie, een TLS-handshake). 1.200 requests openen ~1.200 connecties (de harness telt `connections`). Het endpoint retourneert een constante string, dus de tijd gaat volledig op aan het opzetten en afbreken van connecties.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 15 ref-ms |
| Mediaan gealloceerd | 3 MB |
| Mediane p99-latency | 1 ref-ms |
| connections | ≤ 25 |

> De exercise draait een ASP.NET Core-server op loopback **binnen het harness-proces** (`WebRig`) en stuurt die aan met virtuele gebruikers. Allocatie en CPU bevatten de kleine constante clientkosten.
