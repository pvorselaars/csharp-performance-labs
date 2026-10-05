# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

Gebruik *beide* modi en vergelijk. Sampling laat zien waar de CPU *is*; tracing voegt exacte call counts toe. Geen van beide kan een methode laten zien die de JIT heeft geïnlined: er is geen call meer om te zien, dus de tijd ervan telt mee bij de caller.
</details>

<details><summary>Hint 2: waar?</summary>

Kijk in het sampling-profiel **binnenin** de dictionary-lookup: wat is het hete kind-frame, en ligt het *aantal* `Equals`-calls per lookup rond de één, of veel hoger?
</details>

<details><summary>Hint 3: waarom?</summary>

`Dictionary` vertrouwt erop dat `GetHashCode` keys spreidt over buckets. Als veel verschillende keys dezelfde hash delen, degraderen lookups tot het aflopen van een lange keten `Equals`-calls. Controleer de `GetHashCode` van de key. (Bevestig het mechanisme door `Equals`-calls te tellen.)
</details>
