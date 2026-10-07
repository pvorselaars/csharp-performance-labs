# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

De profiler wijst de loop aan. `perf stat -e branches,branch-misses` vertelt het verhaal: een groot deel van de branches wordt verkeerd voorspeld.
</details>

<details><summary>Hint 2: waar?</summary>

Er is precies één data-afhankelijke branch in de loop. Hoe voorspelbaar is die als de data willekeurig is? En als de data gesorteerd is?
</details>

<details><summary>Hint 3: waarom?</summary>

Een moderne CPU gokt de uitkomst van branches om zijn pipeline gevuld te houden; een foute gok spoelt ~15–20 cycles werk weg. Willekeurige 50/50-data is onvoorspelbaar (ongeveer de helft van de branches wordt misvoorspeld); gesorteerde data is bijna perfect voorspelbaar. De analyse hangt niet af van de volgorde van de metingen, dus je mag ze herordenen.
</details>
