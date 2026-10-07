# L01-05 - Klantdashboard

*De getallen tellen op (langzaam)*

## Symptoom
Een samenvatting bouwen voor 40.000 klanten duurt ongeveer **150 ms**. Als je de code leest gebeurt elke stap
één keer: actieve klanten filteren, scoren, tellen, scores optellen, de topklant vinden. Er wordt weinig
toegewezen en er zijn geen opvallende hot loops, maar het is ruwweg 3x trager dan de hoeveelheid "echt werk"
doet vermoeden.

## Doel
Dezelfde samenvatting (checksum), en:

| Budget | Waarde     |
|---|-----------|
| Mediane tijd | 50 ref-ms |
| Mediaan aantal allocaties | 5 MB      |

## Let op
Deze is met opzet lastig te zien in een *sampling*-profiel: er is niets "mis" met een individueel frame. Het
is een goede oefening in het kiezen van de juiste profiler-modus. Probeer beide en vergelijk wat elk je vertelt.
