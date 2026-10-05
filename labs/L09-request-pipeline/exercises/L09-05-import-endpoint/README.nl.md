# L09-05 - Import endpoint

*Speciale Bezorging*

## Symptoom
Een partner post orderbatches naar `POST /import`: 1.000 regels per batch, ~170 KB JSON met volledige productdetails. De import heeft alleen het id en de hoeveelheid van elke regel nodig, en het model van het endpoint declareert ook alleen die. 1.200 batches van 24 gebruikers duren **~480 ms** met een p99 van **~25 ms**, wijzen **~1,1 GB** toe (zo'n 900 KB per request, meer dan vijf keer de body), en veroorzaken **~35 gen2-collecties** per run. De handler is drie regels: lees de body, parse hem, tel de hoeveelheden op.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediaan tijd | 340 ref-ms |
| Mediaan toegewezen geheugen | 250 MB |
| Mediaan gen2-collecties | ≤ 2 |
| Mediaan p99-latency | 15 ref-ms |

> Deze exercise start een ASP.NET Core-server op loopback **binnen het harness-proces** en stuurt er virtuele gebruikers op af (`PerfLab.Harness.Web.WebRig`). Latency (p50/p99) komt uit het perspectief van de client op elke request.
**Allocatie en CPU bevatten de kleine, constante kosten van de load-genererende client**, behandel allocatiebudgetten dus als "server + client". Omdat beide dezelfde machine delen, zijn de resultaten minder exact dan bij de console-exercises. `taskset -c 0-7 dotnet run ...` vermindert ruis.
