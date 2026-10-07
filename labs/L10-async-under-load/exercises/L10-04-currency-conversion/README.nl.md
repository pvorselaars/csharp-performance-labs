# L10-04 - Valuta-conversie

*Exchange Rates May Vary*

## Symptoom
Een valuta-conversie-endpoint haalt de koers op bij een externe service (10 ms). Bij 50 gebruikers is **het hele endpoint beperkt tot ~100 requests per seconde en ligt de p99 op een halve seconde of meer**, terwijl er maar vier verschillende valuta's zijn en de koersen nauwelijks veranderen.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 5 ref-ms |
| Mediaan toegewezen | 2 MB |
| Mediane p99-latency | 1 ref-ms |

> De exercise draait een ASP.NET Core-server op loopback **binnen het harness-proces** en stuurt die aan met virtuele gebruikers (`WebRig`). Het *minimum* aantal threads van de thread pool staat voorafgaand aan elke run vast op 4 (`Workload.Reset`, hulpconstructie, niet de fix), zodat het effect niet afhangt van je aantal cores. Allocatie en CPU omvatten de kleine constante clientkosten.
