# L11-01 - Orders-dashboard

*Dashboard Confessional*

## Symptoom
Een dashboard-endpoint telt de orders van 20 klanten op. Onder belasting is de p99 een veelvoud van de kosten van één query. *Van buitenaf* (logs, traces) zie je honderden bijna-identieke statements per seconde. De harness telt ze (`sqlCommands`).

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 94 ref-ms |
| Mediaan toegewezen | 62 MB |
| Mediane p99-latency | 12 ref-ms |
| sqlCommands | ≤ 601 |

> De exercise draait een ASP.NET Core-server op loopback **binnen het harness-proces** (`WebRig`) en stuurt die aan met virtuele gebruikers. Databases zijn in-memory SQLite, één keer geseed. Allocatie en CPU bevatten de kleine constante client-kosten.
