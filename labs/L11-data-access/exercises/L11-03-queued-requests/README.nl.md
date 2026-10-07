# L11-03 - Idle database, requests in de wachtrij

*Waiting in Line*

## Symptoom
De database heeft het bij lange na niet druk (elke query duurt 1 ms) maar bij 64 gebruikers gaan **requests in de wachtrij voor een connection**, vlakt de doorvoer af rond 250 requests/seconde en is de p99 enorm. In productie laat hetzelfde patroon zich zien als `Timeout expired... all pooled connections were in use`. Threads en CPU zijn idle.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 1330 ref-ms |
| Mediaan toegewezen | 6 MB |
| Mediane p99-latency | 161 ref-ms |

> De exercise draait een ASP.NET Core-server op loopback **binnen het harness-proces** (`WebRig`) en stuurt die aan met virtuele gebruikers. Databases zijn in-memory SQLite, één keer geseed. Allocatie en CPU bevatten de kleine constante client-kosten.
