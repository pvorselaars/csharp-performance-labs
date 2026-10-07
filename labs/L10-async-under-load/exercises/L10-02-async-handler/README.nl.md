# L10-02 - Async handler

*Busy Doing Nothing*

## Symptoom
De handler is gedeclareerd als `async` en awaitet iets, dus hij *lijkt* non-blocking. Maar bij 200 gelijktijdige gebruikers ligt zijn p99 ver boven de 15 ms "werk", en de CPU staat stil. De doorvoer zit vast op een klein veelvoud van het aantal threads in de pool.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 100 ref-ms |
| Mediaan toegewezen | 3 MB |
| Mediane p99-latency | 20 ref-ms |

> De exercise draait een ASP.NET Core-server op loopback **binnen het harness-proces** en stuurt die aan met virtuele gebruikers (`WebRig`). Het *minimum* aantal threads van de thread pool staat voorafgaand aan elke run vast op 4 (`Workload.Reset`, hulpconstructie, niet de fix), zodat het effect niet afhangt van je aantal cores. Allocatie en CPU omvatten de kleine constante clientkosten.
