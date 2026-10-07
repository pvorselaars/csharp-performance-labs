# L10-06 - Achtergrondtaken

*Take a Number*

## Symptoom
Een endpoint accepteert een taak en draait hem op de achtergrond, zodat het antwoord snel komt. Bij een burst van 300 requests draaien er **honderden taken tegelijk**, en vertragen de HTTP-requests (de p99 loopt op). De taken komen af, maar de server was intussen onresponsief. De harness rapporteert `peakJobs`.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 1439 ref-ms |
| Mediaan toegewezen | 3 MB |
| Mediane p99-latency | 7 ref-ms |
| peakConcurrentJobs | ≤ 7 |

> De exercise draait een ASP.NET Core-server op loopback **binnen het harness-proces** en stuurt die aan met virtuele gebruikers (`WebRig`). Het *minimum* aantal threads van de thread pool staat voorafgaand aan elke run vast op 4 (`Workload.Reset`, hulpconstructie, niet de fix), zodat het effect niet afhangt van je aantal cores. Allocatie en CPU omvatten de kleine constante clientkosten.
