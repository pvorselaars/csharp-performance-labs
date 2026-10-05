# L12-04 - Oplossing

## Grondoorzaak
Identieke, cachebare responses worden bij elke request opnieuw berekend.

## Fix
`AddOutputCache` + `UseOutputCache` + `.CacheOutput()` op het endpoint (met een zinnig policy: expiratie, vary-by, tags voor invalidatie).

## Lessen
1. **De snelste request is er een die je niet draait.** Cache op de hoogste laag die correct is.
2. Bepaal het invalidatieverhaal (expiry, tags, events) voordat je gaat cachen.
3. Varieer op wat de response verandert (query, headers, user); een onvoldoende gespecificeerde cache-key bedient verkeerde data.
4. Output caching helpt alleen voor responses die veilig te delen zijn; cache geen per-user of gevoelige data zonder vary/autorisatie.

## Extra credit
Voeg een `[Authorize]`-achtige per-user-variatie toe (`VaryByHeader`) en zie de hit rate dalen.

## Ga verder
Voeg een policy toe met een expiratie van 1 seconde en een tag; evict op tag bij een `POST`. Wat doet p99 op de expiry-grens?
