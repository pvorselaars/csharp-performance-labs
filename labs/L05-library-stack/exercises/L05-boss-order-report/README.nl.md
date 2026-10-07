# L05-boss - Orderrapport

*The Full Stack of Mistakes*

## Symptoom
Een nachtelijk rapport telt de orders van elke klant op en schrijft per klant een audit-regel weg. Voor 300 klanten duurt dat **~50 ms en wijst ~8 MB toe**: veel meer dan de data (1.800 order-rijen) rechtvaardigt. Van buitenaf zie je honderden bijna-identieke SQL-statements, veel geheugen voor een klein resultaat, en duizend bestandsoperaties.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 2 ref-ms |
| Mediaan toegewezen | 1 MB |

## Eindbaas-gevecht
Dit is de **eindbaas** van zijn lab: een vermomde combinatie van de defecten uit dat lab, in een ander domein, **zonder hints per defect**. Profileer hem, noteer wat je vindt, fix één ding per keer, en schrijf daarna op **uit welke exercise elk defect kwam** (de oplossing somt ze op). Slagen betekent *alle* budgetten halen.
