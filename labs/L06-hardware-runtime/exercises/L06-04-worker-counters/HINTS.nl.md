# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

De timeline toont acht drukke threads en geen blocking. Het bewijs zit op hardwareniveau: `perf stat` toont zwaar cache-coherence-verkeer; `perf c2c` rapporteert betwiste cache lines.
</details>

<details><summary>Hint 2: waar?</summary>

Welk geheugen schrijft elke thread, en hoe ver uit elkaar liggen die adressen? Een `long` is 8 bytes.
</details>

<details><summary>Hint 3: waarom?</summary>

CPU's verplaatsen geheugen in cache lines van 64 bytes, en slechts één core mag een line tegelijk voor schrijven vasthouden. Acht aangrenzende `long`s delen één line, dus elke increment steelt de line van een andere core: **false sharing**. Scheid de tellers met minstens een cache line.
</details>
