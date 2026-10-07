# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

Alleen tijd zit boven budget, geheugen niet. Dat wijst op een **sampling**-CPU-profiel, niet op een memory profiler.
</details>

<details><summary>Hint 2: waar?</summary>

Kijk naar de top self-time-frames en loop *omhoog* naar het eerste frame dat jouw code is. Vraag je dan af:
hoe vaak wordt de callee van dat frame per rij aangeroepen? Is dat aantal constant, of verandert het naarmate
de import vordert?
</details>

<details><summary>Hint 3: waarom?</summary>

Voor elke rij scan je een collectie die blijft groeien. Totaal werk ≈ rijen × (gemiddelde grootte van de
collectie). Welke datastructuur beantwoordt "heb ik dit al eerder gezien?" in min of meer constante tijd,
terwijl hoofdletterongevoelige vergelijking toch behouden blijft?
</details>
