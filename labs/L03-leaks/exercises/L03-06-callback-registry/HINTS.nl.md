# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

dotMemory snapshot compare: wat groeide? Het aantal `byte[]` (20.000) komt overeen met het aantal callbacks.
</details>

<details><summary>Hint 2: waar?</summary>

Volg het retentiepad vanaf een `byte[]`: registry → delegate → **closure-object** → array. Wat zit er in de closure?
</details>

<details><summary>Hint 3: waarom?</summary>

Een lambda legt de *variabelen die hij gebruikt* vast in een door de compiler gegenereerd closure-object dat net zo lang leeft als de delegate. `report` werd vastgelegd omdat de lambda hem leest. Leg alleen de kleine waarde vast die je nodig hebt (kopieer hem eerst naar een local) en de grote array kan verzameld worden.
</details>
