# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

Sampling toont alle tijd binnen LINQ's `Count` en de lambda (`<>c.<Run>b__…`). Kijk daarna naar de machinecode die de JIT voor die loop produceerde (`DOTNET_JitDisasm`, zie de profiling guide): één `cmp` per element.
</details>

<details><summary>Hint 2: waar?</summary>

Per element doet de trage versie een *indirecte call naar de lambda*, dan een vergelijking en een conditionele increment. Wat zou de CPU 8 vergelijkingen in één instructie laten doen?
</details>

<details><summary>Hint 3: waarom?</summary>

Moderne CPU's hebben vectorregisters (AVX2: 8 ints). De BCL vectoriseert al veelvoorkomende operaties over spans (`Count`, `IndexOf`, `Contains`, `Sum`…). Zoek in `MemoryExtensions` naar de operatie voordat je zelf SIMD gaat schrijven.
</details>
