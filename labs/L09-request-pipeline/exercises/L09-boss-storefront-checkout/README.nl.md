# L09-boss - Storefront checkout

*Lange Rij bij Kassa Één*

## Symptoom
Een checkout-endpoint accepteert een winkelmandje van ~60 KB en antwoordt met een bon. **Per request wijst het honderden KB toe**, veroorzaakt gen2-collecties, en duurt veel langer dan het werk rechtvaardigt. Een middleware, een DI-call, het lezen van de body en het schrijven van de response zien er elk op zich redelijk uit.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediaan tijd | 300 ref-ms |
| Mediaan toegewezen geheugen | 320 MB |
| Mediaan gen2-collecties | 0 |
| Mediaan p99-latency | 15 ref-ms |

> Deze exercise start een ASP.NET Core-server op loopback **binnen het harness-proces** en stuurt er virtuele gebruikers op af (`PerfLab.Harness.Web.WebRig`). Latency (p50/p99) komt uit het perspectief van de client op elke request.
**Allocatie en CPU bevatten de kleine, constante kosten van de load-genererende client**, behandel allocatiebudgetten dus als "server + client". Omdat beide dezelfde machine delen, zijn de resultaten minder exact dan bij de console-exercises. `taskset -c 0-7 dotnet run ...` vermindert ruis.

## Eindbaas-gevecht
Dit is de **eindbaas** van dit lab: een vermomde combinatie van de defecten uit dit lab, in een ander domein, met **geen hints per defect**. Profileer hem, maak een lijst van wat je vindt, fix één ding tegelijk, en schrijf daarna op **uit welke exercise elk defect kwam** (de oplossing somt ze op). Slagen betekent *alle* budgetten halen.
