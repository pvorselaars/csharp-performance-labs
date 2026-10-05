# L10-boss - Async quotes

*Quote, Unquote, Timeout*

## Symptoom
Een quote-endpoint heeft bij 100 gelijktijdige gebruikers een **p99 in de honderden milliseconden** (een losse request duurt ~30 ms), de CPU staat stil, en een downstream-service ziet **honderden calls tegelijk onderweg**.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 2164 ref-ms |
| Mediaan toegewezen | 9 MB |
| Mediane p99-latency | 621 ref-ms |
| peakInflight | ≤ 61 |

## Eindbaas-gevecht
De **eindbaas** van zijn lab: een vermomde combinatie van de defecten uit dat lab, met **geen hints per defect**. Profileer, noteer wat je vindt, fix één ding tegelijk, en schrijf daarna op **uit welke exercise elk defect afkomstig was** (de oplossing noemt ze). Slagen betekent *alle* budgets halen.
