# L12-05 - Flaky downstream

*Als het regent, giet het*

## Symptoom
Een downstream service kan tot 20 gelijktijdige calls aan en faalt daarboven snel - maar "snel falen" houdt toch een van de 25 workers bezig voor de volledige duur van de call (een echt overbelaste backend wijst niet gratis af: een verbinding, een thread, een timeout kosten allemaal iets). Bij 80 gebruikers **lopen downstream-calls op tot een veelvoud van het aantal requests**, en veel requests falen nog steeds (`giveUps`). Elk van die extra calls concurreert met echt verkeer om dezelfde 25 workers, dus **de retry storm faalt niet alleen meer - hij is ook trager als geheel**, niet enkel luidruchtiger.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 1000 ref-ms |
| Mediaan toegewezen | 2 MB |
| Mediane p99-latency | 200 ref-ms |
| downstreamCalls | 400 |
| giveUps | 0 |

> De exercise draait een ASP.NET Core-server op loopback **binnen het harness-proces** (`WebRig`) en stuurt hem aan met virtuele gebruikers. Databases zijn in-memory SQLite, één keer geseed. Allocatie en CPU bevatten de kleine constante clientkosten.
