# L07-02 - Oplossing

## Wat het profiel laat zien
- **`dotnet-counters`:** `gc-heap-size` plat, `working-set` oplopend; **dotMemory/`dumpheap -stat`:** kleine `NativeBuffer`-objecten, niets groots.
- **`pmap`/`smaps`:** veel anonieme mappings van ~1 MB.

## Grondoorzaak
`NativeBuffer` wijst unmanaged geheugen toe (`Marshal.AllocHGlobal`) maar geeft het nooit vrij: geen `Dispose`, geen finalizer. De GC ruimt de wrapper op en de pointer gaat verloren; het blok lekt voor de rest van de levensduur van het proces. Dit is onzichtbaar voor elke managed-heap-tool.

## Fix
Maak de wrapper `IDisposable`, geef het blok vrij in `Dispose` (idempotent, via `Interlocked.Exchange`), onderdruk de finalizer, houd een finalizer aan als vangnet, en gebruik `using` op de call-sites. Geef in nieuwe code de voorkeur aan een van `SafeHandle` afgeleid type, zodat het patroon correct is door constructie.

## Lessen
1. **Managed heap plat + procesgeheugen stijgend = unmanaged geheugen.** Native libraries, `Marshal.Alloc*`, memory-mapped files, thread-stacks en groei van JIT/metadata leven allemaal buiten `gc-heap-size`.
2. Vergelijk eerst *managed* counters met *proces*-counters; dat vertelt je welke tool-familie je daarna nodig hebt (managed snapshots vs `pmap`, native profilers).
3. Eigenaarschap: wie unmanaged geheugen toewijst, heeft een release-pad nodig dat niet vergeten kan worden (`SafeHandle`, `IDisposable` + `using`, analyzers CA2000/CA1001).
4. De GC kan hier niet helpen: `GC.Collect()` reduceert hier niets.

## Go further
Vervang de class door een van `SafeHandle` afgeleid type. Wat hoef je daarna niet meer te schrijven?
