# L09-03 - Report endpoint

*Meldplicht*

## Symptoom
Een report-endpoint geeft 300 korte regels terug (ongeveer 9 KB). Het duurt per request veel langer dan de omvang doet vermoeden...

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediaan tijd | 35 ref-ms |
| Mediaan toegewezen geheugen | 100 MB |
| Mediaan p99-latency | 3 ref-ms |

> Deze exercise start een ASP.NET Core-server op loopback **binnen het harness-proces** en stuurt er virtuele gebruikers op af (`PerfLab.Harness.Web.WebRig`). Latency (p50/p99) komt uit het perspectief van de client op elke request.
**Allocatie en CPU bevatten de kleine, constante kosten van de load-genererende client**, behandel allocatiebudgetten dus als "server + client". Omdat beide dezelfde machine delen, zijn de resultaten minder exact dan bij de console-exercises. `taskset -c 0-7 dotnet run ...` vermindert ruis.
