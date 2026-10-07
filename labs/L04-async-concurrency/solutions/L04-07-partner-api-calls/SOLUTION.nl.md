# L04-07 - Oplossing

## Wat het profiel laat zien
> Ter illustratie: de profiler-weergaves zijn wat de code impliceert (er is geen echte profiler-opname gemaakt).

- **Kloktijd ≈ items × latency** (serieel), CPU en aantal threads vlak.

## Grondoorzaak
Een concurrency-limiet van 1 maakt van een parallelliseerbare I/O-workload een seriële.

## Fix
Verhoog `MaxDegreeOfParallelism` tot wat de downstream aankan (hier 32), na te hebben gemeten waar diens latency begint te stijgen.

## Lessen
1. **Grenzen kennen twee faalwijzen:** te los overbelast de dependency (L04-03), te strak verspilt hem.
2. Wet van Little: doorvoer = concurrency ÷ latency. Ken je er twee, dan ken je de derde.
3. Kies limieten op basis van meting en herzie ze als de dependency verandert.
4. Maak de limiet configureerbaar en observeerbaar (een in-flight-meter).

## Extra credit
Wat is de *kleinste* mate van parallellisme die binnen 200 ms klaar is?

## Go further
Veeg de mate van parallellisme van 1 tot 256 en plot de totale tijd. Waar stopt hij met verbeteren?
