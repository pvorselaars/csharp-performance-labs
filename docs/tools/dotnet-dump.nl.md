# dotnet-dump

**Beantwoordt:** vrijwel alles, achteraf. Een dump is een kopie van het geheugen van het proces: elk object met zijn velden, elke thread met zijn stack, elke lock. Je neemt hem één keer en inspecteert hem daarna offline, zo lang je wilt.

## Commando's
```bash
dotnet-dump collect -n <id> -o app.dmp                 # volledige dump (standaard)
dotnet-dump analyze app.dmp                            # interactieve prompt; typ 'help', 'exit' om te verlaten

# of draai commando's niet-interactief
dotnet-dump analyze app.dmp -c "dumpheap -stat" -c "exit"
```

## Commando's binnen `analyze`
| Commando | Toont |
|---|---|
| `dumpheap -stat` | elk type op de heap: aantal en totale grootte, kleinste eerst (dus de grote staan onderaan) |
| `dumpheap -stat -min 100000` | alleen objecten van 100 KB of meer (grote buffers) |
| `dumpheap -mt <MT>` | elk object van één type (de `MT`-kolom uit `-stat`) |
| `dumpobj <address>` | de velden van één object |
| `gcroot <address>` | de keten van referenties die een object in leven houdt: **het** lek-commando |
| `clrthreads` | managed threads, en welke de GC- en finalizer-threads zijn |
| `clrstack -all` | de managed stack van elke thread |
| `pstacks` | stacks samengevoegd op call path: "40 threads zitten allemaal in `Task.Wait`" in één oogopslag |
| `syncblk` | welke threads een `lock` bezitten, en hoeveel erop wachten |
| `threadpool` | aantal thread pool-workers en queue-lengte |
| `dumpasync` | async state machines op de heap, voor "vastgelopen" async-code |
| `finalizequeue` | objecten die wachten op finalisatie |

Een typische lekjacht: `dumpheap -stat` → kies het verdachte type → `dumpheap -mt <MT>` → neem één adres → `gcroot <address>` → lees de keten van de root tot aan je object.

## Valkuilen
- **Dumps zijn groot.** Een volledige dump van een kleine exercise was 176 MB. `--type Heap` is kleiner en heeft nog steeds de hele managed heap. `--type Mini` heeft alleen threads en stacks.
- **Een dump bevat alles in geheugen**, inclusief geheimen en klantgegevens in een echte service. Behandel het als een database-export.
- Het proces staat stil terwijl de dump wordt geschreven.
- Open de dump op hetzelfde OS waarop hij genomen is. Een Linux-dump kun je niet met `dotnet-dump` op Windows analyseren, en andersom ook niet.

## Documentatie
[dotnet-dump (Microsoft Learn)](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/dotnet-dump) · [SOS-commando's](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/sos-debugging-extension)
