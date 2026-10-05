# L04-02 - Latency stats

*Many Hands, Slow Work*

## Symptoom
Acht workers schrijven 320.000 latency-samples weg naar één gedeelde recorder. Het werk is embarrassingly parallel, maar de run duurt ongeveer even lang als wanneer je het **op één thread** zou doen. De CPU-meter laat ruwweg één core bezig zien, geen acht.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 73 ref-ms |
| Mediaan toegewezen | 1 MB |

## Opmerking
Vereist **minimaal 4 cores** om het effect te laten zien. De exercise start zijn eigen threads/tasks, dus het resultaat hangt niet af van hoe druk jouw machine is, maar een 2-core machine zal de verbetering onderrapporteren.
