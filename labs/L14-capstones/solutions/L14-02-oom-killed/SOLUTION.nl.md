# L14-02 - Oplossing

## Wat het profiel laat zien
> Illustratief: profiler-views zijn wat de code impliceert (geen echte profiler-capture).

- **Behouden:** ~1.200 × 100 KB `byte[]` geworteld vanuit de `MemoryCache`. **Gen2:** aangedreven door LOH-allocatie.

## Grondoorzaak
LOH-grote garbage per request *en* een onbegrensde cache van het hele gerenderde object: geheugen groeit onbeperkt en elke allocatie triggert volledige collecties.

## Fix
Huur scratch-buffers van `ArrayPool`, cache alleen het kleine antwoord (begrensd in grootte, met een vervaltijd), en geef de buffer terug. Daarna is het geheugen vlak en verdwijnen de gen2-collecties.

## Lessen
1. **OOM-kills zijn meestal groei, geen piek.** Kijk naar behouden geheugen na GC en naar wat het vasthoudt (een cache).
2. Twee bekende defecten uit verschillende labs stapelen op: LOH-churn (gen2-stormen) en onbegrensde retentie.
3. Zet een geheugenlimiet in de testomgeving en alarmeer op de trend van `gc-heap-size`, niet alleen op de piek.
4. Schrijf de post-mortem: welke metric had je *vóór* de kill kunnen waarschuwen?

## Extra credit
Welk eviction-beleid past bij deze workload: LRU/grootte, TTL, of beide, en hoe zou je de limiet dimensioneren?

## Go further
Zet een containergeheugenlimiet (`systemd-run --user --scope -p MemoryMax=…`) en draai de trage versie onder belasting: hoe sterft hij, en na hoeveel requests?
