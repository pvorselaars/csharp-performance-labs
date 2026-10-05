# L12-01 - Downstream call

*Bel een vriend*

## Symptoom
Een endpoint roept een downstream service aan. **De callee ziet bij vrijwel elke request een nieuwe TCP-verbinding** (de harness telt losse verbindingen: `connections`). Op schaal wordt dit poortuitputting, opeenhoping van TIME_WAIT en TLS-handshake-CPU.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 8 ref-ms |
| Mediaan toegewezen | 3 MB |
| Mediane p99-latency | 1 ref-ms |
| connections | 16 |

> De exercise draait twee aparte ASP.NET Core-servers op loopback **binnen het harness-proces** (`WebRig`): het endpoint onder test, en de downstream service die het aanroept. Allocatie en CPU bevatten de kleine constante clientkosten.
