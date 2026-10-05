# Antwoorden

| Limiet / setting | Resultaat | Geziene `limit=` | Allocaties (6 s run) |
|---|---|---|---|
| none (baseline) | prima | 33.486 MB (host-RAM) | ≈ 10–14 M (8 s: 14,0 M) |
| 260M | prima | ≈ 235 MB | 10,2 M |
| 240M | prima | – | 9,5 M |
| 220M | prima, **~30% trager** | – | 6,9 M |
| 200M | **crasht: "Out of memory", exitcode 134 (abort)** | – | – |
| 220M + `GCHeapHardLimit=0xC800000` (200 MB) | prima, **doorvoer hersteld** | – | 10,0 M |
| 220M + `GCConserveMemory=9` | prima, er tussenin | – | 8,6 M |

1. De runtime leest de **cgroup-geheugenlimiet** af en zet een standaard **heap hard limit van 75% daarvan** (300M → 235 MB hier, zichtbaar als `limit=` via `GC.GetGCMemoryInfo().TotalAvailableMemoryBytes`).
2. Met de standaardcap van 75% van 220 MB ≈ 173 MB en 150 MB live data is er maar ~23 MB speelruimte voor garbage, dus draait de GC veel vaker en werkt harder om onder de cap te blijven: doorvoer daalt ~30%.
3. Onder het punt waarop live data plus een minimale working set past onder de *eigen* cap van de GC (75% van 200 MB = 157 MB vs. 150 MB live), **raakt de GC zijn hard limit en gooit/abort met OutOfMemory voordat de OOM-killer van de kernel ingrijpt**. In andere opstellingen zie je de kernel-kill (exitcode 137).
4. Een expliciete `GCHeapHardLimit` van 200 MB laat meer ruimte voor de heap en minder voor al het andere in de container (runtime, JIT, thread stacks, native libraries). Het risico: de *kernel* killt het proces zodra het totale gebruik de containerlimiet overschrijdt, zonder waarschuwing en zonder managed exception. Laat ruimte over voor non-heap-geheugen en meet de working set van het proces, niet alleen de GC-heap.
5. Voor een limiet van 256 MB en 150 MB live data: vergroot de *speelruimte voor de GC* (expliciete heap-limiet rond 75–80% van de limiet, of `GCHeapHardLimitPercent`), verklein de working set (pooling, kleinere buffers, workstation GC), of verhoog de containerlimiet. Bevestig met dezelfde loadtest op de doellimiet: doorvoer, `gc-heap-size`, GC-pauzetijd, en geen OOM over meerdere uren.

## De les
Een geheugenlimiet is geen limiet waar je onder past of niet: naarmate je hem nadert wordt de GC drukker, lang voordat iets daadwerkelijk faalt. Test **op** de limiet, niet alleen eronder.
