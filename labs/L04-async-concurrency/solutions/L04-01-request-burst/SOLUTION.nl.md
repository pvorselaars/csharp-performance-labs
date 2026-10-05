# L04-01 - Oplossing

## Wat het profiel laat zien

- **Timeline:** pool-threads grotendeels *Waiting* (in `Task.Wait`/`GetResult`), het aantal threads kruipt omhoog, CPU ≈ 0.
- **`dotnet-counters`:** `threadpool-queue-length` > 0 terwijl de CPU laag blijft: de signatuur van starvation.
- **Sampling** zou bijna niets laten zien: geblokkeerde threads staan niet op de CPU, dus een CPU-profiel kan het probleem niet zien.

## Grondoorzaak
`Handle` blokkeert een pool-thread met `.Result` op een task die *een andere* pool-thread nodig heeft om af te ronden. Onder een burst raken geblokkeerde threads de pool uitgeput, de runtime voegt geleidelijk nieuwe threads toe, en requests wachten in de rij achter elkaar: **thread-pool starvation**. De latency groeit mee met de burst-grootte; de CPU blijft idle.

## Fix
Ga helemaal async: `HandleAsync` awaitet de repository-call, zodat er geen thread wordt vastgehouden tijdens die 20 ms. De enige blokkerende wait die overblijft zit helemaal aan de rand (de harness); in een webapp awaitet het framework je `Task` voor je.

## Lessen
1. **Traag maar idle CPU -> verdenk wachten**, en sampling kan wachten niet zien: gebruik de timeline en de pool-tellers.
2. Sync-over-async riskeert ook regelrechte deadlocks waar een synchronization context bestaat (UI, oude ASP.NET).
3. Eén blokkerende call fixen is niet genoeg als een caller hogerop blokkeert op jouw `Task`: async moet helemaal tot de top van de call-chain doorlopen.
4. 'Fix' starvation niet met `SetMinThreads` in productie; dat verbergt het blokkeren en kost geheugen en context switches.

## Extra credit
Voorspel de latency van de traagste request bij 400 requests voordat je het draait (in een scratch-kopie). Is de groei lineair?

## Go further
Verhoog `SetMinThreads` naar 200 in een scratch-kopie: het 'werkt'. Vergelijk het aantal threads en het geheugengebruik met de async-versie, en leg uit waarom dat een slechte ruil is.
