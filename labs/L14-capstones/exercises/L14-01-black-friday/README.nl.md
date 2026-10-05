# L14-01 - Black Friday

*Doorbusters*

## Symptoom
Op lanceerochtend treft een golf gelijktijdige shoppers de pagina's van een handvol populaire producten. Het eigen dashboard van de externe pricing-service laat veel meer verkeer zien dan de storefront zou moeten sturen, en een reëel deel van die calls faalt ronduit. De latency tijdens de piek is veel slechter dan op een rustige ochtend. Het dashboard van elk team wijst naar andermans service.

## Doel
Zelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 150 ref-ms |
| Mediaan toegewezen | 2,3 MB |
| Mediane p99-latency | 100 ref-ms |
| externalCalls | ≤ 46 |
| giveUps | ≤ 1 |

## Capstone-regels
Alleen het symptoom; meerdere defecten, elk verbergt de volgende. Schrijf de post-mortem (`templates/POSTMORTEM.md`) **voordat** je de oplossing leest. Hints zijn met opzet generiek.
