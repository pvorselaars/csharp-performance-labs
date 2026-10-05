# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

Sampling, gesorteerd op self time. Daarna tracing-modus om te zien hoe vaak `Sort` wordt aangeroepen en met hoeveel elementen.
</details>

<details><summary>Hint 2: waar?</summary>

Het top-frame zit binnen `List<T>.Sort`. Loop omhoog naar je loop: hoe vaak wordt die aangeroepen, en hoe groot is de lijst elke keer?
</details>

<details><summary>Hint 3: waarom?</summary>

n elementen sorteren kost O(n log n); dat na *elke* insert doen is in totaal O(n² log n). Wat *leest* de loop eigenlijk, en wat is de goedkoopste structuur die precies die vraag beantwoordt?
</details>
