# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

Sampling laat zien *waar tijd heen gaat*, maar niet *hoe vaak* iets draaide. Probeer **tracing**-modus, die
exacte aantallen aanroepen vastlegt. Vergelijk het aantal aanroepen van de scoringsmethode met het aantal actieve klanten.
</details>

<details><summary>Hint 2: waar?</summary>

De scoringsmethode wordt een veelvoud van het aantal actieve klanten aangeroepen. Zoek elke plek in
`DashboardBuilder.Build` die `scored` consumeert, en tel ze.
</details>

<details><summary>Hint 3: waarom?</summary>

`Where(...).Select(...)` berekent niets op het moment dat je het schrijft: het beschrijft een query. Wat
gebeurt er elke keer dat je die query een nieuwe vraag stelt (`Any`, `Count`, `Sum`, `OrderBy`)?
Zie ook: Rider/ReSharper signaleren dit patroon statisch ("possible multiple enumeration").
</details>
