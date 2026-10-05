# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

dotMemory allocations, plus de harness-kolommen: gen0, gen1 en gen2 zijn **allemaal** aan elkaar gelijk. Gewone kortlevende rommel zou veel gen0 en weinig gen2 geven. Welk soort allocatie maakt elke collectie een gen2?
</details>

<details><summary>Hint 2: waar?</summary>

De allocatie is `new byte[PageBytes]` in `Render`. Zoek de groottedrempel voor de Large Object Heap op en vergelijk die met `PageBytes`.
</details>

<details><summary>Hint 3: waarom?</summary>

Objecten van 85.000 bytes of meer komen op de LOH terecht, die alleen door gen2-collecties wordt opgeruimd. Er één per call alloceren betekent constante gen2-GC's. De array is alleen nodig voor de duur van de call. Welk BCL-type leent arrays uit? Lees de documentatie over wat een *geleende* array bevat, en hoe lang hij is.
</details>
