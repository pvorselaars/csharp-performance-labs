# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

Eerst de timeline (idle CPU, trage requests = wachten), dan `dotnet-counters` voor de thread-pool- en lock-contention-counters. Pas daarna sampling.
</details>

<details><summary>Hint 2: waar?</summary>

Volg één request: waar wacht hij precies *op*, en wie houdt dat vast? Als je het eerste probleem dat je vindt fixt, meet dan opnieuw: het volgende probleem ziet er anders uit (contention, dubbel werk, sequentieel wachten).
</details>

<details><summary>Hint 3: waarom?</summary>

Hier liggen lagen van de eerdere labs op elkaar gestapeld: blocking op async, een lock die wordt vastgehouden tijdens een trage operatie, misses die allemaal hetzelfde opnieuw berekenen, en sequentiële awaits die concurrent hadden gekund. Fix ze in de volgorde waarin het bewijs ze blootlegt.
</details>
