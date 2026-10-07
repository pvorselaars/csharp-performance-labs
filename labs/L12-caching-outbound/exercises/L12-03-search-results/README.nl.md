# L12-03 - Search results

*Zoek en gij zult vinden*

## Symptoom
Een endpoint cachet zoekresultaten per query. Met 3.000 losse queries in een run blijft ongeveer **24 MB bereikbaar na een volledige GC** en dit blijft groeien met de variatie in verkeer: op een echte site met miljoenen losse queries groeit dit door tot de container wordt gekilld. De code gebruikt een echte cache-library, geen handgerolde dictionary.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 50 ref-ms |
| Mediaan toegewezen | 70 MB |
| Mediane p99-latency | 1 ref-ms |
| Behouden na een volledige GC | ≤ 5 MB |

> De exercise draait een ASP.NET Core-server op loopback **binnen het harness-proces** (`WebRig`) en stuurt hem aan met virtuele gebruikers. Databases zijn in-memory SQLite, één keer geseed. Allocatie en CPU bevatten de kleine constante clientkosten.
