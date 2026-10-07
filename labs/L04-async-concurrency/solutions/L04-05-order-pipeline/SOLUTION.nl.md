# L04-05 - Oplossing

## Wat het profiel laat zien
- **Metric:** `maxQueued` ≈ 25844
- **Metric:** `peakHeapMb` ≈ 254 Mb
- **allocaties:** het aantal levende `byte[]` klimt en daalt weer naarmate de consumer leegt.

## Grondoorzaak
Een onbegrensde queue tussen stages die niet bij elkaar passen. Niets remt de producer af, dus de queue-lengte wordt alleen begrensd door het geheugen.

## Fix
`Channel.CreateBounded(capacity)` met `FullMode = Wait`, en `await WriteAsync` in de producer. De producer draait nu op het tempo van de consumer, en het geheugengebruik is begrensd tot `capacity x itemgrootte`.

## Lessen
1. **Elke queue heeft een grens nodig.** Een onbegrensde queue verbergt overbelasting totdat het een storing wordt.
2. Back-pressure plant zich voort: een trage consumer vertraagt de producer, die op zijn beurt *zijn* caller kan vertragen: zo vertelt het systeem je iets over capaciteit.
3. Kies de grens op basis van geheugenbudget en acceptabele latency, volgens de wet van Little (queue-lengte x bedieningstijd = wachttijd). Voeg load shedding toe wanneer wachten niet acceptabel is.
4. Hetzelfde idee op elke schaal: thread-pool-queues, message-broker-prefetch, HTTP-accept-queues.

## Extra credit
Maak de consumer 4x sneller. Waar stabiliseert `maxQueued` zich voor de *onbegrensde* versie, en waarom?

## Go further
Zet `FullMode` op `DropOldest` en leg uit wat je daarmee opgeeft. Wanneer is droppen de juiste keuze?
