# L12-boss - Catalog service

*Cache Me Outside*

## Symptoom
Een productendpoint wordt voorafgegaan door een cache, maar toch ziet de backend **bij vrijwel elke load een nieuwe TCP-verbinding**, herhaalde uitbarstingen van dubbele loads voor hetzelfde populaire product elke keer dat de cache-entry verloopt, en na een run blijft **~10 MB bereikbaar na een volledige GC**: dit groeit met elk eenmalig product-id ooit opgevraagd. Geheugen en verbindingen lopen allebei op met de *variatie* in verkeer, en de herhaalde stampedes maken het erger bij elke cache-expiry, niet alleen eenmalig bij opstart.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 1800 ref-ms |
| Mediaan toegewezen | 100 MB |
| Mediane p99-latency | 128 ref-ms |
| connections | ≤ 49 |
| Behouden na een volledige GC | ≤ 11 MB |

> De exercise draait twee aparte ASP.NET Core-servers op loopback **binnen het harness-proces** (`WebRig`): de productservice onder test, en een catalogus-backend waar deze van afhangt. Allocatie en CPU bevatten de kleine constante clientkosten.

## Eindbaas-gevecht
De **eindbaas** van zijn lab: een vermomde combinatie van de defecten van dat lab met **geen hints per defect**. Profileer, noteer wat je vindt, fix één ding tegelijk, en schrijf achteraf op **uit welke exercise elk defect kwam** (de oplossing somt ze op). Slagen betekent *alle* budgetten halen.
