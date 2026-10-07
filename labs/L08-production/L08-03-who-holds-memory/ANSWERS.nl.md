# Antwoorden

1. **Bytes:** `System.Byte[]` domineert (in mijn capture 1,9 GB over 237.101 arrays), maar dat zijn alleen de payloads. De *groeiende types* zijn **`Session`** (237.000 instanties), zijn `List<string>` en `string[]`/`string` (`Trail`) en de **entries-array** van de `Dictionary<Guid, Session>` (10 MB en groeiend). De aantallen van `Session`, `Byte[]` (de 8.000-byte exemplaren) en de dictionary-entries komen overeen: één gelekte `Session` per request.
2. **Constant:** ~40 `Byte[]` van 500.000 bytes (een catalogus van 20 MB, eenmalig geladen), en een queue van ~1.000 kleine `Audit`-objecten. Geen van beide verandert tussen de twee dumps.
3. **Root:** static field **`Requests.ActiveSessions`**, een `Dictionary<Guid, Session>`: `gcroot` toont *static variable -> Dictionary → Entry[] -> Session -> ...*.
4. De catalogus wordt eenmalig geladen en nooit aangevuld; de audit-queue is *begrensd* (oude items worden gedequeued bij 1.000). `ActiveSessions` voegt alleen maar toe.
5. "Verwijder sessies uit `ActiveSessions` als ze eindigen (verlopen of uitloggen), of begrens het (LRU/TTL)." (Dit is de fix van L03-02.)
6. Een test of alert op **groei van live bytes na een volledige GC** tijdens een gelijkmatige workload (de kept-after-GC-gate van deze harness; `dotMemory Unit` in een unit test; een productie-alert op `gc-heap-size` bij een stijgende gen2-trend).

## Opmerking over `dumpheap -stat` vs. `dotnet-gcdump`
`dumpheap -stat` op de ruwe dump telde ook 12.681 `Audit`-objecten (niet de 1.000 live exemplaren): de dump bevat garbage die nog niet is opgeruimd. `dotnet-gcdump` triggert eerst een GC en toont alleen live objecten. Weet welke van de twee je leest.
