# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

Alles idle: het is noch CPU, noch GC. Vergelijk de kloktijd met (calls × latency) en met (calls × latency ÷ concurrency).
</details>

<details><summary>Hint 2: waar?</summary>

Welke optie of constante bepaalt hoeveel calls er tegelijk in flight zijn? Welke waarde heeft die?
</details>

<details><summary>Hint 3: waarom?</summary>

Lab 4 leerde je dat onbegrensde concurrency gevaarlijk is; dit is het tegenovergestelde falen. Doorvoer = concurrency ÷ latency. Kies de limiet op basis van de *capaciteit van de downstream* (meet de knik: L04-03), niet uit voorzichtigheid.
</details>
