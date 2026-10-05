# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

dotMemory snapshot **Compare** over een run heen. Welk type is verantwoordelijk voor de groei, en wat is de *dominator* die ze vasthoudt?
</details>

<details><summary>Hint 2: waar?</summary>

De dominator is één static collection. Lees de declaratie en elke plek die eraan toevoegt. Is er een plek die entries verwijdert?
</details>

<details><summary>Hint 3: waarom?</summary>

Een cache zonder eviction-policy is een lek met een vriendelijke naam. Wat is de *working set* die de code daadwerkelijk nodig heeft (hoe ver terug reiken lookups)? Kies een grens op basis daarvan, en kies dan een eviction-policy: grootte (LRU), leeftijd (TTL), of beide.
</details>
