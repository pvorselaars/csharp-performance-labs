# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

Sampling-profiel in Release. Volg het heetste pad vanaf `ParseAll` naar beneden. Negeer de data-generatiecode;
kijk wat er *binnen* de aanroep gebeurt die je loop voor elke cel doet.
</details>

<details><summary>Hint 2: waar?</summary>

Frames die bij de exception-machinerie van de runtime horen (throwen, stack-trace vastleggen, unwinden,
catch-dispatch) verschijnen onder de parse-aanroep. Namen variëren een beetje tussen .NET-versies. Vergelijk
ook het "toegewezen MB" in de harness met hoe weinig data er eigenlijk is.
</details>

<details><summary>Hint 3: waarom?</summary>

Een exception gooien is ordes van grootte duurder dan een waarde teruggeven, en dat gebeurt voor ruwweg elke
tweede cel. Is "deze cel is geen getal" hier een uitzonderlijke situatie, of een verwachte? Kijk naar het
"Try…"-patroon in de BCL.
</details>
