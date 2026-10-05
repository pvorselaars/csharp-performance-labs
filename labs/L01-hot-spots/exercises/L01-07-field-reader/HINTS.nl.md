# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

Eerst sampling (welke frames?), dan allocaties per type.
</details>

<details><summary>Hint 2: waar?</summary>

Binnen de per-item-loop: welke aanroepen gaan over het *vinden* van de property, in plaats van hem *lezen*?
</details>

<details><summary>Hint 3: waarom?</summary>

Reflection heeft twee kostenposten: het lid opzoeken en het aanroepen (met boxing van het resultaat). Geen
van beide hangt af van het item, dus doe de lookup één keer. Converteer daarna naar een typed delegate (of een
`switch`/source generator) zodat de kosten per item een gewone aanroep zijn.
</details>
