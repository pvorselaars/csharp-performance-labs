# L00-01 - Activity feed

*Trainingswielen*

> **Dit is Lab 0: een complete, al uitgewerkte exercise.** Doe hem één keer met [`docs/worked-example/`](../../../../docs/worked-example/README.md)
> ernaast open, om te zien hoe een afgeronde poging eruitziet: elk template ingevuld. Begin daarna aan Lab 1 en vul je eigen in.

## Symptoom
Het bouwen van een activity feed van 60.000 items duurt ongeveer **190 ms**. De feed toont nieuwste eerst, en de code
voert precies één simpele bewerking per item uit. De allocatie is klein (ongeveer 2 MB) en de GC draait nooit. Het is dus geen geheugenprobleem; er wordt ergens CPU verbrand.

## Doel
Dezelfde feed (checksum), en:

| Budget | Waarde     |
|---|-----------|
| Mediane tijd | 29 ref-ms |
| Mediane allocatie | 8 MB      |
