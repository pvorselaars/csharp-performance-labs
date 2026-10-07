# L03-02 - Oplossing

## Wat het profiel laat zien
- **snapshots:** +40.000 `Session` en +40.000 `byte[]` (2.000 bytes). Dominator: de static `Dictionary<long, Session>` in `Sessions`.

## Grondoorzaak
`Sessions.Cache` is een static dictionary die alleen maar toevoegt. Session-id's zijn uniek, dus niets wordt ooit overschreven, en lookups reiken alleen tot de meest recente 200 sessies, dus ~99,5% van de entries wordt nooit meer gelezen maar blijft voor altijd bereikbaar.

## Fix
Begrens de cache. Hier: een LRU met capaciteit 1.000 (ruim boven de working set van 200 sessies, dus het gedrag blijft ongewijzigd). Geef in een echte app de voorkeur aan `MemoryCache` met `SizeLimit` en expiratie, of `HybridCache`, boven het zelf bouwen; de LRU wordt getoond zodat het mechanisme zichtbaar is.

## Take-aways
1. **Elke cache heeft een eviction-policy en een groottegrens nodig.** Als je niet kunt zeggen wat een entry laat vertrekken, heb je een lek geschreven.
2. Kies de grens op basis van het *toegangspatroon*, niet een gok: meet hoe ver terug lookups daadwerkelijk reiken.
3. Onbegrensde caches beginnen vaak als een 'voor nu even' dictionary; maak 'cache' een type met een capaciteit zodat het niet per ongeluk kan gebeuren.
4. Een cache die te klein is, uit zich als extra belasting elders; houd naast geheugen ook de hit rate in de gaten.

## Extra credit
Zet de capaciteit op 100 (onder de working set van 200 sessies). Wat gebeurt er met de checksum, en wat zegt dat over het kiezen van een grens?

## Go further
Vervang de LRU door `MemoryCache` (`SizeLimit` + `SetSize`) en vergelijk het gedrag. Voeg daarna een TTL toe. Wat gaat er stuk als twee threads tegelijk `Set` aanroepen?
