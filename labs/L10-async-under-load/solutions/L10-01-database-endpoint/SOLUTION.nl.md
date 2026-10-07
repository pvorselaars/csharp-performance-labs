# L10-01 - Oplossing

## Wat het profiel laat zien
- **Counters:** thread-pool queue length > 0 en groeiend thread count, CPU bijna stil. **Timeline:** threads in `Task.Wait`/`GetResult`.

## Grondoorzaak
`.Result` blokkeert een pool-thread voor de duur van elke call, waardoor de pool onder concurrency uitgeput raakt (thread-pool starvation).

## Fix
`async`/`await` in de handler; zowel minimal APIs als MVC awaiten je `Task`, dus niets blokkeert.

## Lessen
1. **De doorvoer van de webserver wordt beperkt door *geblokkeerde threads*, niet door CPU.** Async handlers schalen mee met concurrency; blokkerende handlers schalen met thread count.
2. Sync-over-async in één hot endpoint kan de *hele* server uithongeren, inclusief ongerelateerde endpoints (ook health checks lopen dan time-out).
3. Dezelfde signatuur als L04-01: idle CPU, queueing, groeiend thread count: lees de counters, niet de CPU-grafiek.
4. Fix niet met `SetMinThreads`; fix het blokkeren.

## Extra credit
Verhoog het aantal gebruikers naar 400. Hoe schalen de p99's van de twee versies?

## Verder
Voeg een tweede endpoint `/health` toe dat direct antwoordt. Meet zijn p99 terwijl het trage endpoint onder belasting staat, in beide versies.
