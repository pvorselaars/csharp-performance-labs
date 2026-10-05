# L05-04 - Service calls

*Please Hold*

## Symptoom
Het doen van 300 kleine requests naar een service duurt **~50 ms**, en dat is grotendeels *niet* jouw code en *niet* het werk van de server. De server rapporteert **honderden losse TCP-verbindingen** voor wat één aanroeper is. Tegen een echte server over TLS kost elke nieuwe verbinding een netwerk-round-trip of meer.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 20 ref-ms |
| Mediaan toegewezen | 4 MB |
| connections | ≤ 5 |

## Opmerking
De exercise start een piepkleine HTTP-server op `127.0.0.1` binnen het proces, en de harness telt de **distincte TCP-verbindingen** die hij ziet (`connections`). Echte servers zijn trager dan loopback, dus het effect in de praktijk is groter dan wat je hier meet.
