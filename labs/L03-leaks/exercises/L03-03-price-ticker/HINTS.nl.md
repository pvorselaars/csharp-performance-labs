# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

dotMemory snapshot compare, en dan **Key retention paths** op een overlevende `Ticker`. De root zal geen van je eigen fields zijn.
</details>

<details><summary>Hint 2: waar?</summary>

Volg het pad vanaf de root: het loopt door de timer-machinerie van de runtime. Wat heeft de `Ticker`-constructor aangemaakt waar de runtime een reference naar vasthoudt?
</details>

<details><summary>Hint 3: waarom?</summary>

Een lopende `System.Threading.Timer` wordt door de runtime in leven gehouden totdat hij gedisposed wordt of zijn periode afloopt, en zijn callback (een lambda die `this` gebruikt) verwijst naar de `Ticker`. Het laten vallen van de laatste reference naar de `Ticker` maakt hem dus geen garbage. Wat moet je aanroepen?
</details>
