# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

Sampling wijst de loop en de piepkleine `Area`-methodes aan. `perf stat -e branch-misses` (misvoorspelde *indirecte* branches), en JIT-disassembly die een indirecte call per element laat zien.
</details>

<details><summary>Hint 2: waar?</summary>

De loop roept `s.Area()` aan via een interface op elementen van *drie verschillende types in willekeurige volgorde*. Kan de JIT op dat aanroeppunt weten welke methode het zal zijn?
</details>

<details><summary>Hint 3: waarom?</summary>

Met één dominant type devirtualiseert en inlinet dynamic PGO de aanroep. Met drie types in willekeurige volgorde is het doel van de indirecte branch onvoorspelbaar, dus de CPU voorspelt vaak fout, en er kan niets geïnlined worden. Verwerk je de vormen **gegroepeerd per concreet type**, dan is elke loop monomorf en worden de bodies geïnlined.
</details>
