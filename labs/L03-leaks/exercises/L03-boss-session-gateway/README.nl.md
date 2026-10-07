# L03-boss - Session gateway

*Alles Moet Weg (Niets Gaat Weg)*

## Symptoom
Een gateway verwerkt 1.500 sessies per batch. Na een batch blijven **tientallen megabytes bereikbaar na een volledige GC**, hoewel niets zijn eigen sessie zou moeten overleven. De groei heeft meer dan één vorm: een deel zijn hele objecten, een deel zijn buffers waarvan je je niet herinnert dat je ze vasthield. (Testopstelling ruimt de gedeelde state tussen runs op; dat is niet de fix.)

## Doel
Zelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 4 ref-ms |
| Mediaan toegewezen | 24 MB |
| Behouden na een volledige GC | ≤ 1 MB |

## Eindbaas-gevecht
Dit is de **eindbaas** van dit lab: een vermomde combinatie van de defecten uit dat lab, in een ander domein, met **geen hints per defect**. Profileer het, maak een lijst van wat je vindt, fix één ding tegelijk, en schrijf daarna op **uit welke exercise elk defect kwam** (de oplossing noemt ze). Slagen betekent *alle* budgetten halen.
