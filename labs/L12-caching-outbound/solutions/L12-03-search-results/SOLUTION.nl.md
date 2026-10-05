# L12-03 - Oplossing

## Wat het profiel laat zien
- **Retained:** duizenden strings geworteld vanuit de entry-tabel van de cache.

## Grondoorzaak
Een cache zonder size limit of expiry, geïndexeerd op input met hoge cardinaliteit: een memory leak by design (L03-02 op een echte host).

## Fix
`SizeLimit` op de cache, `SetSize` op elke entry, en een expiratie. Monitor hit rate en eviction count.

## Lessen
1. **Elke cache heeft een grens en een expiry nodig**; kies ze op basis van het geheugenbudget en het toegangspatroon.
2. Keys met hoge cardinaliteit (zoektekst, user-id's, URL's) zijn het gevaar.
3. `SizeLimit` zonder `SetSize` gooit een exception: het framework dwingt je na te denken over entry-kosten.
4. Houd `hit rate` in de gaten: een te kleine grens maakt van de cache pure overhead.

## Extra credit
Zet `SizeLimit = 50` en draai opnieuw. Wat is de hit rate, en helpt het nog steeds?

## Ga verder
Vervang door `HybridCache` en configureer `MaximumPayloadBytes`. Waar woont de grens nu?
