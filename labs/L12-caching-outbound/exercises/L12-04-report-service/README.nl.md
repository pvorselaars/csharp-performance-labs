# L12-04 - Report service

*Vers van de pers*

## Symptoom
Een report-endpoint heeft maar 20 losse outputs en die veranderen zelden, maar toch herberekent elke request het rapport van 8 ms. Bij 32 gebruikers wordt de doorvoer van het endpoint begrensd door de kosten van het rapport, en doet de backend telkens hetzelfde werk opnieuw.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 10 ref-ms |
| Mediaan toegewezen | 6 MB |
| Mediane p99-latency | 1 ref-ms |

> De exercise draait een ASP.NET Core-server op loopback **binnen het harness-proces** (`WebRig`) en stuurt hem aan met virtuele gebruikers. Databases zijn in-memory SQLite, één keer geseed. Allocatie en CPU bevatten de kleine constante clientkosten.
