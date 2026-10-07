# L05-01 - Klantorders

*So Many Questions*

## Symptoom
Een rapport telt de orders van 500 klanten op. Het duurt hier ~60 ms (en tegen een echte database over een netwerk veel langer) en stuurt **honderden SQL-commando's** voor wat conceptueel één vraag is. Lees je de C# door, dan oogt elke regel redelijk.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 2 ref-ms |
| Mediaan toegewezen | 1 MB |
| sqlCommands | 1 |

## Opmerking
De database is een in-memory SQLite, gedeeld door de hele run, één keer geseed. De harness telt de SQL-commando's die jouw code verstuurt (`sqlCommands`). Heeft je profiler een SQL-/ADO.NET-subsysteem-view (Rider's dotTrace heeft die), open die en vergelijk met de telling; anders vertelt EF's eigen command-logging je hetzelfde.
