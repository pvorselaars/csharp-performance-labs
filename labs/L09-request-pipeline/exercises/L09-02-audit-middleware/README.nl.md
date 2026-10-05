# L09-02 - Audit middleware

*Tolpoortje bij elke request*

## Symptoom
Een kleine audit-middleware draait op elke request: hij tagt de request, logt hem, en voegt een header toe. Het log-niveau zorgt ervoor dat **er nooit iets geschreven wordt**. Toch wijst hij per request **meerdere KB** toe en verbrandt hij CPU, terwijl het endpoint erachter een eenregelaar is. De kosten zitten allemaal in de 15 regels middleware.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediaan tijd | 30 ref-ms |
| Mediaan toegewezen geheugen | 17 MB |
| Mediaan p99-latency | 1 ref-ms |

> Deze exercise start een ASP.NET Core-server op loopback **binnen het harness-proces** en stuurt er virtuele gebruikers op af (`PerfLab.Harness.Web.WebRig`). Latency (p50/p99) komt uit het perspectief van de client op elke request.
**Allocatie en CPU bevatten de kleine, constante kosten van de load-genererende client**, behandel allocatiebudgetten dus als "server + client". Omdat beide dezelfde machine delen, zijn de resultaten minder exact dan bij de console-exercises. `taskset -c 0-7 dotnet run ...` vermindert ruis.
