# L14-02 - OOM-killed om 3 uur 's nachts

*Nachtdienst*

## Symptoom
In productie wordt de container **om de paar uur OOM-killed**, 's nachts wanneer het batchverkeer binnenkomt. Geen enkel request is traag, en per request lijkt er niets mis. De geheugengrafiek vertelt een ander verhaal: het is een trap die alleen maar omhoog gaat, en een volledige GC brengt hem nauwelijks terug naar beneden.

## Doel
Zelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 42 ref-ms |
| Mediaan toegewezen | 10 MB |
| Mediane gen2-collecties | ≤ 2 |
| Mediane p99-latency | 6 ref-ms |
| Behouden na een volledige GC | ≤ 2 MB |

## Capstone-regels
Alleen het symptoom; meerdere defecten, elk verbergt de volgende. Schrijf de post-mortem (`templates/POSTMORTEM.md`) **voordat** je de oplossing leest. Hints zijn met opzet generiek.
