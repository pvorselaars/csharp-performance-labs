# L06-04 - Oplossing

## Wat het profiel laat zien
- **Sampling/timeline:** alle threads on-CPU, 100% in de loop; geen wachten.
- **`perf stat`/`perf c2c`:** zwaar cross-core cache-line-verkeer op één line.

## Grondoorzaak
De acht tellers staan in aangrenzende array-elementen, dus ze delen één cache line van 64 bytes. Onafhankelijke atomic increments vanaf verschillende cores vechten alsnog om het eigendom van die line (MESI-coherence-verkeer).

## Fix
Pad zodat elke teller op zijn eigen cache line staat (stride van 8 `long`s). Nog beter: accumuleer in een lokale variabele en schrijf het gedeelde slot één keer (helemaal geen sharing meer).

## Lessen
1. **False sharing: onafhankelijke data, gedeelde cache line.** Ziet eruit als parallellisme, gedraagt zich als contention.
2. Geen profiler toont dit als een wachttijd; kijk naar *scaling* (meer threads, geen snelheidswinst) en naar hardware-counters.
3. Padding kost geheugen; gebruik het voor hot, per-thread data. Thread-lokaal accumuleren en aan het eind samenvoegen is eenvoudiger en sneller.
4. De grootte van het effect hangt af van de CPU-topologie (cores die een cache-complex delen versus cores die dat niet doen): meet op de doelhardware.

## Extra credit
Draai met 2, 4 en 8 threads (gedeelde versie). Bij welk aantal threads verschijnt de penalty op jouw machine, en hangt dat af van welke cores gebruikt worden (probeer `taskset -c 0-3` versus `taskset -c 0,10`)?

## Ga verder
Herschrijf met een lokale `long` per thread die aan het eind wordt samengevoegd, en vergelijk met de padded versie. Probeer stride 4 en 16: waar verdwijnt het effect, en wat zegt dat over de line-grootte van jouw CPU?
