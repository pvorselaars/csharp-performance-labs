# L13-02 - Request logging

*Dear Diary*

## Symptoom
De service logt vier regels per request via een kleine custom file logger. De latency is niet wat hij zou moeten zijn.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 53 ref-ms |
| Mediaan gealloceerd | 25 MB |
| Mediane p99-latency | 7 ref-ms |

## Opmerking (de ASP.NET Core-labs (9–14) harness)
De exercise draait een ASP.NET Core-server op loopback **binnen het harness-proces** (`WebRig`) en stuurt die aan met virtuele gebruikers. Allocatie en CPU bevatten de kleine constante clientkosten.
