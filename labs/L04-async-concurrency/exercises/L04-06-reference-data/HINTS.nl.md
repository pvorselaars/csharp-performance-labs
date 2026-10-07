# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

Timeline: threads *geblokkeerd op een monitor*; de contention-teller is torenhoog. Eén lookup is snel, maar acht threads die dat non-stop doen maken van één lock het hele programma.
</details>

<details><summary>Hint 2: waar?</summary>

`Get` neemt de lock voor een read. Waartegen moet een lezer eigenlijk beschermd worden?
</details>

<details><summary>Hint 3: waarom?</summary>

Lezers hebben alleen bescherming tegen schrijvers nodig. Als een schrijver een gepubliceerd object nooit wijzigt (maar een nieuw object bouwt en een referentie omwisselt), hebben lezers helemaal geen lock nodig. Dat is copy-on-write. Wanneer is dat een slecht idee? (Denk aan: hoe groot is het object, en hoe vaak wordt er geschreven?)
</details>
