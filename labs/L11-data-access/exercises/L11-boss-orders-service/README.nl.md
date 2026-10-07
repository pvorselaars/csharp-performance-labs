# L11-boss - Orders-service

*Query Storm*

## Symptoom
Een klant-detail-endpoint geeft een ordertotaal terug. Bij 32 gebruikers is de **p99 een veelvoud van één request** en tonen de database-logs **~20 statements per request**, plus queries die **honderden rijen** teruggeven voor een klant met slechts 40 gerelateerde records. De connection pool is verzadigd terwijl de database nauwelijks bezig is.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 350 ref-ms |
| Mediaan toegewezen | 35 MB |
| Mediane p99-latency | 40 ref-ms |
| sqlCommands | ≤ 3 |

## Eindbaas-gevecht
De **eindbaas** van zijn lab: een vermomde combinatie van de defecten van dat lab, **zonder hints per defect**. Profileer, noteer wat je vindt, fix één ding tegelijk, en schrijf achteraf op **uit welke exercise elk defect afkomstig was** (de oplossing noemt ze). Slagen betekent dat je *alle* budgetten haalt.
