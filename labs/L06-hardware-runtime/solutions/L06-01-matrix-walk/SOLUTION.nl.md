# L06-01 - Oplossing

## Wat het profiel laat zien

- **Sampling:** 100% self time op de ene optelregel, een onhandig maar eerlijk resultaat: de kosten zitten in *wachten op geheugen*, onzichtbaar als instructies.
- **`perf stat`:** veel meer `cache-misses` en een veel lagere instructions-per-cycle voor de column-major volgorde.

## Grondoorzaak
De array ligt rij voor rij in het geheugen, maar de loops doorlopen hem kolom voor kolom. Elke access landt op een nieuwe cache line, dus de CPU stalt bijna elke iteratie op geheugen; het ALU-werk stelt niets voor.

## Fix
Wissel de loop-volgorde om zodat de binnenste loop opeenvolgend geheugen doorloopt (`row` buiten, `col` binnen). Dezelfde optellingen, hetzelfde resultaat.

## Lessen
1. **Het geheugentoegangspatroon doet er vaak meer toe dan het aantal instructies.** Zelfde werk, andere volgorde, meerdere keren sneller.
2. Een profiler toont de regel maar niet de stall: vorm de hypothese, bevestig daarna met counters (`perf stat`).
3. Row-major (C#, C, Java): doorloop de *laatste* index het snelst. Column-major-talen (Fortran, MATLAB) zijn precies andersom.
4. Het verschil groeit met de datagrootte: de array moet groter zijn dan de cache wil het effect dramatisch zijn.

## Extra credit
Verklein N naar 256 (de grid past in de cache) in een scratch-kopie. Hoeveel maakt de loop-volgorde nu nog uit?

## Ga verder
Probeer een geblokte (tiled) doorloop voor een *transpose*, waarbij zowel lezen als schrijven niet sequentieel kunnen zijn. Welke tile-grootte werkt het best op jouw CPU?
