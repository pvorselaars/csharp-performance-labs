# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

De sampling-profiler vertelt je *waar* (die ene regel) maar niet *waarom*. Tijd om te hypothetiseren over de **hardware**. Op Linux geeft `perf stat -e cache-misses,cache-references` rond de run bewijs; BenchmarkDotNet kan op Linux geen hardware-counters uitlezen.
</details>

<details><summary>Hint 2: waar?</summary>

Kijk hoe de twee loops de array indexeren: welke index verandert het snelst, en hoe ver uit elkaar in het geheugen liggen opeenvolgende accesses?
</details>

<details><summary>Hint 3: waarom?</summary>

Geheugen wordt geladen in cache lines van 64 bytes. Een kolom aflopen springt 4096 × 4 bytes = 16 KB per stap, dus elke access raakt een andere cache line (die alweer verdwenen is tegen de tijd dat je terugkomt). Een rij aflopen gebruikt alle 16 ints in elke line, en de hardware-prefetcher kan vooruit streamen.
</details>
