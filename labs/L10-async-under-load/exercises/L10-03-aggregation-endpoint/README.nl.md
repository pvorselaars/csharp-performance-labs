# L10-03 - Aggregatie-endpoint

*The Sum of Its Parts*

## Symptoom
Een aggregatie-endpoint roept per request 30 keer, gelijktijdig, een downstream-service aan en geeft de som terug. Bij 16 gebruikers ziet de downstream **honderden calls tegelijk onderweg**, en elke request duurt ruim **honderd milliseconden**, terwijl één request alleen zo'n 10 ms zou kosten. De latency van de downstream loopt op met de belasting; de harness toont `peakInflight`.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 700 ref-ms |
| Mediaan toegewezen | 3 MB |
| Mediane p99-latency | 70 ref-ms |
| peakInflight | ≤ 50 |

> De exercise draait een ASP.NET Core-server op loopback **binnen het harness-proces** en stuurt die aan met virtuele gebruikers (`WebRig`). Het *minimum* aantal threads van de thread pool staat voorafgaand aan elke run vast op 4 (`Workload.Reset`, hulpconstructie, niet de fix), zodat het effect niet afhangt van je aantal cores. Allocatie en CPU omvatten de kleine constante clientkosten.
