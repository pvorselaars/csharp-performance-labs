# Vragen
Voorspel elke rij *voordat* je hem draait.

| Limiet / setting | Voorspelling: overleeft? doorvoer vs. baseline? | Geobserveerde exitcode | Geobserveerde `limit=` in output | Geobserveerde doorvoer (allocaties) |
|---|---|---|---|---|
| none | | | | |
| 260M | | | | |
| 220M | | | | |
| 200M | | | | |
| 220M + `GCHeapHardLimit=200MB` | | | | |
| 220M + `GCConserveMemory=9` | | | | |

1. Welk geheugenlimiet leest de GC af van de container, en welk deel daarvan staat hij standaard toe aan de *managed heap*? Waar zag je dat?
2. Waarom daalt de doorvoer bij 220M, ook al past het proces nog? Wat doet de GC meer van? (Kijk naar de gen0/gen1/gen2-aantallen.)
3. Waarom **crasht** het proces bij 200M met "Out of memory" in plaats van gekilld te worden door de kernel?
4. Waarom doet een expliciete `GCHeapHardLimit` (200 MB) het beter dan de standaard bij 220M? Wat is het risico van die limiet zo hoog instellen in een echte container?
5. Wat zou je configureren voor een echte service met 150 MB live data en een limiet van 256 MB? Wat zou je meten om dat te bevestigen?
