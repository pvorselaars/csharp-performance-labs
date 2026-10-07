# L10-01 - Database-endpoint

*Rush Hour*

## Symptoom
Bij 200 gelijktijdige gebruikers heeft een endpoint waarvan de database-call 20 ms duurt een **p99-latency in de honderden milliseconden**, en de doorvoer stort in naarmate de concurrency stijgt, terwijl de CPU bijna stilstaat. Met één gebruiker duurt het 20 ms.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 405 ref-ms |
| Mediaan toegewezen | 7 MB |
| Mediane p99-latency | 107 ref-ms |

## Opmerking (de harness van de ASP.NET Core-labs (9–14))
De exercise draait een ASP.NET Core-server op loopback **binnen het harness-proces** en stuurt die aan met virtuele gebruikers (`WebRig`). De thread pool wordt voorafgaand aan elke run beperkt (`Workload.Reset`: minimaal 4 threads, en in L10-01 maximaal 32, als stand-in voor een CPU-beperkte container; dat is hulpconstructie, niet de fix), zodat het effect niet afhangt van je aantal cores. Allocatie en CPU omvatten de kleine constante clientkosten.
