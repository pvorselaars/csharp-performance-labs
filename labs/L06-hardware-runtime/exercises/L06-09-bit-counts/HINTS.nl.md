# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

`perf stat` voor branch-misses; disassembly om de loop te zien.
</details>

<details><summary>Hint 2: waar?</summary>

Hoeveel iteraties draait de binnenste loop voor een willekeurig 64-bit woord, en is het einde ervan voorspelbaar?
</details>

<details><summary>Hint 3: waarom?</summary>

Moderne CPU's hebben een `POPCNT`-instructie en .NET stelt die beschikbaar als `BitOperations.PopCount` (met een software-fallback). Kijk in `System.Numerics.BitOperations` voordat je zelf bit-trucs schrijft.
</details>
