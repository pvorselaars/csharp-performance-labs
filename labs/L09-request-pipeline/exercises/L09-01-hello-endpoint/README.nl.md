# L09-01 - Hello endpoint

*De Duurste Hallo van de Stad*

## Symptoom
Het simpelst denkbare endpoint geeft `{"message":"hello"}` terug. Bij 4.000 requests gebruikt het **meer tijd en geheugen dan de response ooit zou kunnen rechtvaardigen**. Voordat je echte endpoints gaat tunen, moet je weten wat de *bodem* is: wat kost een request als de handler niets doet?

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediaan tijd | 30 ref-ms |
| Mediaan toegewezen geheugen | 12 MB |
| Mediaan p99-latency | 1 ref-ms |

> Deze exercise start een ASP.NET Core-server op loopback **binnen het harness-proces** en stuurt er virtuele gebruikers op af (`PerfLab.Harness.Web.WebRig`). Latency (p50/p99) komt uit het perspectief van de client op elke request.
**Allocatie en CPU bevatten de kleine, constante kosten van de load-genererende client**, behandel allocatiebudgetten dus als "server + client". Omdat beide dezelfde machine delen, zijn de resultaten minder exact dan bij de console-exercises. `taskset -c 0-7 dotnet run ...` vermindert ruis.
