# L11-04 - Catalogus met paginering

*Turn the Page*

## Symptoom
Een endpoint serveert een gepagineerde productcatalogus. Na 400 requests is er nog zo'n **30 MB bereikbaar na een volledige GC**, en dat groeit met elke andere rij die ooit gelezen is. Onder concurrency gaan requests ook achter elkaar in de wachtrij, hoewel de database idle is. Niets in de code ziet eruit als een cache.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 60 ref-ms |
| Mediaan toegewezen | 70 MB |
| Mediane p99-latency | 5 ref-ms |
| Bewaard na een volledige GC | 0 MB |

> De exercise draait een ASP.NET Core-server op loopback **binnen het harness-proces** (`WebRig`) en stuurt die aan met virtuele gebruikers. Databases zijn in-memory SQLite, één keer geseed. Allocatie en CPU bevatten de kleine constante client-kosten.
