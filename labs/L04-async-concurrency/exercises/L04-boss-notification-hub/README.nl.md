# L04-boss - Notification hub

*You've Got Mail (Eventually)*

## Symptoom
Een notificatieservice heeft drie stages per batch: sessies registreren inboxen op een gedeelde bus, workers leggen bezorgstatistieken vast, en uitgaande berichten worden gequeued voor een tragere verzender. De batch is traag, **geheugen dat vrijgegeven had moeten zijn is na een volledige GC nog steeds bereikbaar**, en **de queue groeit naar duizenden items**. Niets in de drie stages lijkt duidelijk met elkaar te maken te hebben.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 113 ref-ms |
| Mediaan toegewezen | 37 MB |
| maxQueued | ≤ 76 |
| Bereikbaar na een volledige GC | ≤ 1 MB |

## Eindbaas-gevecht
Dit is de **eindbaas** van zijn lab: een **vermomde combinatie** van defecten uit dit lab, in een ander domein. Er zijn geen hints per defect. Profileer hem, noteer wat je vindt, fix één ding tegelijk, en schrijf daarna op **uit welke eerdere exercise elk defect kwam** (de oplossing somt ze op). Slagen betekent *alle* budgetten halen.

De harness bewaakt tijd, allocatie, **geheugen dat na een volledige GC nog bereikbaar is**, en **queue-diepte**. Vereist minimaal 4 cores.
