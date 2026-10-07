# L09-04 - Price endpoint

*De Prijs Is Juist*

## Symptoom
`GET /price/{id}?qty=N` geeft een prijsopgave: zoek de stuksprijs van het product op, vermenigvuldig, pas een bulkkorting toe. 1.500 requests van 32 gebruikers duren **~620 ms** met een p99 van **~35 ms**, en wijzen **~890 MB** toe: zo'n 600 KB per request, voor een antwoord van een paar bytes. De prijslijst zelf is klein en verandert nooit terwijl de app draait.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediaan tijd | 20 ref-ms |
| Mediaan toegewezen geheugen | 6 MB |
| Mediaan p99-latency | 1 ref-ms |

> Deze exercise start een ASP.NET Core-server op loopback **binnen het harness-proces** en stuurt er virtuele gebruikers op af (`PerfLab.Harness.Web.WebRig`). Latency (p50/p99) komt uit het perspectief van de client op elke request.
**Allocatie en CPU bevatten de kleine, constante kosten van de load-genererende client**, behandel allocatiebudgetten dus als "server + client". Omdat beide dezelfde machine delen, zijn de resultaten minder exact dan bij de console-exercises. `taskset -c 0-7 dotnet run ...` vermindert ruis.
