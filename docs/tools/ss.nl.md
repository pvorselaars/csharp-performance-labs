# ss
> Alleen Linux

**Beantwoordt:** welke TCP-verbindingen bestaan er, en in welke staat? Gebruik het als een service traag is met *verbinden* in plaats van traag met rekenen, of om te zien of verbindingen hergebruikt worden of steeds opnieuw geopend.

`ss` zit standaard in Linux (package `iproute2`). Geen installatie nodig.

## Commando's
```bash
ss -tan                                  # alle TCP-sockets (-t TCP, -a alle staten, -n numerieke poorten)
ss -tan state established                # alleen open verbindingen
ss -Htan state time-wait | wc -l         # tel sockets in TIME-WAIT (-H laat de headerregel weg)
ss -s                                    # totalen per staat
ss -tanp                                 # toon ook het eigenaar-proces (pid)
```
Draai een telling **voor** en **na** een exercise (of terwijl hij in profile-modus draait) en vergelijk.

## Hoe je het leest
- **ESTAB:** open verbindingen. Een client die verbindingen hergebruikt, houdt er een paar van open.
- **TIME-WAIT:** verbindingen die net gesloten zijn. De kant die als eerste sluit, houdt deze ongeveer een minuut vast. Honderden of duizenden ervan betekenen dat verbindingen met hoge frequentie geopend en gesloten worden.
- Het **peer-adres en de poort** vertellen je welke kant welke is. De testservers van de exercises luisteren op `127.0.0.1` met een willekeurige poort.

## Valkuilen
- Andere programma's op je machine hebben ook verbindingen. Filter op poort (`ss -tan '( dport = :5000 or sport = :5000 )'`) of vergelijk voor en na.
- TIME-WAIT-sockets overleven het proces. Wacht een minuut tussen runs, anders tel je ook de vorige run mee.
- **Windows:** `netstat -an | findstr TIME_WAIT`, of in PowerShell `Get-NetTCPConnection -State TimeWait`.

## Documentatie
[ss man page](https://man7.org/linux/man-pages/man8/ss.8.html)
