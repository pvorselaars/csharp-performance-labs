# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

De kept-after-GC-regel van de harness; dotMemory snapshot-vergelijking: groei van `Product` en `string`, gedomineerd door de change tracker van de DbContext (`StateManager`).
</details>

<details><summary>Hint 2: waar?</summary>

Welk object bezit de bewaarde `Product`s, en waarom laat het ze nooit los?
</details>

<details><summary>Hint 3: waarom?</summary>

Een trackende `DbContext` houdt voor zijn hele levensduur een referentie (en een snapshot) vast voor elke entiteit die hij laadt. Een context is bedoeld als **unit of work**: kortlevend. Een langlevende is een trage lek en een concurrency-bug (`DbContext` is niet thread-safe).
</details>
