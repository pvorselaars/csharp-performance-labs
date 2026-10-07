# L06-boss - Sensor grid

*Know Your Silicon*

## Symptoom
Een sensor-verwerkingsstap berekent kolomtotalen van een 2048×2048-grid, een gewogen som over één miljoen schaarse metingen, en een telling van een specifieke code onder acht miljoen. Samen duren ze **~30 ms**; diezelfde drie loops "zouden" een fractie daarvan moeten kosten. Er zijn geen allocaties in het hot path en geen voor de hand liggend trage aanroepen.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 7 ref-ms |
| Mediaan gealloceerd | 0 MB |

## Eindbaas-gevecht
Dit is de **eindbaas** van zijn lab: een **vermomde combinatie** van defecten uit dit lab, in een ander domein. Er zijn geen hints per defect. Profileer hem, noteer wat je vindt, fix één ding tegelijk, en schrijf daarna op **uit welke eerdere exercise elk defect kwam** (de oplossing noemt ze). Slagen betekent *alle* budgetten halen.
