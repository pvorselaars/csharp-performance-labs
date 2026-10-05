# L12-02 - Popular items

*Openingsavond*

## Symptoom
Vijf populaire items worden gecachet in `IMemoryCache` met een loader van 50 ms. Wanneer de cache koud is (na een deploy, een eviction, of een herstart), komen 40 gelijktijdige requests tegelijk binnen en **draait elk van hen de loader**: de harness telt via `loads` tientallen loads voor 5 losse keys. De backend achter de loader kan maar 8 calls tegelijk bedienen (een connection pool, een rate limit); de rest wacht in de rij. 40 overbodige loads in plaats van 5 betekent extra wachttijd, dus elke aanroeper - niet alleen de overbodige - wacht langer.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 120 ref-ms |
| Mediaan toegewezen | 1 MB |
| Mediane p99-latency | 120 ref-ms |
| loads | ≤ 9 |

> De exercise draait een ASP.NET Core-server op loopback **binnen het harness-proces** (`WebRig`) en stuurt hem aan met virtuele gebruikers. Databases zijn in-memory SQLite, één keer geseed. Allocatie en CPU bevatten de kleine constante clientkosten.
