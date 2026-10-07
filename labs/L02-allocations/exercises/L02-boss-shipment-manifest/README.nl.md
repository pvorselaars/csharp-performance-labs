# L02-boss - Verzendmanifest

*Careful Code, Careless Heap*

## Symptoom
Een nachtelijke job zet 20.000 verzendregels om in een manifest en een gewichtstotaal. Dat duurt **ongeveer 30 ms en alloceert 150 MB**, en beide getallen zijn slechter dan de taak verdient. De code leest als gewone, zorgvuldige C#.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 5 ref-ms |
| Mediaan gealloceerd | 1 MB |

## Eindbaas-gevecht
Dit is de **eindbaas** van zijn lab: een **vermomde combinatie** van defecten uit dit lab, in een ander domein. Er zijn geen hints per defect. Profileer hem, noteer wat je vindt, fix één ding tegelijk, en schrijf daarna op **van welke eerdere exercise elk defect afkomstig was** (de oplossing somt ze op). Slagen betekent *alle* budgetten halen.