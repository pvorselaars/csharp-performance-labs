# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

Gebruik dotMemory's **allocation**-view (niet alleen dotTrace). Bekijk de allocaties gegroepeerd **op type**. Wat is het meest voorkomende type, en komt dat ergens overeen met iets in de *brondata*?
</details>

<details><summary>Hint 2: waar?</summary>

Het meest voorkomende type is een primitief type. Zoek de code die waarden opslaat of ophaalt via `object`. Er zijn drie aparte plekken, niet één: twee collecties, en één bewerking op een van die twee.
</details>

<details><summary>Hint 3: waarom?</summary>

Een `int` in een `object`-slot stoppen *boxt* hem: een nieuw heap-object, 24 bytes voor een waarde van 4 bytes. Non-generieke collecties (`ArrayList`, `Hashtable`) slaan `object` op. Sorteren erop loopt ook via `IComparable`. Welke generieke collecties houden de waarden als `int`?
</details>
