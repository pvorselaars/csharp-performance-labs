# Hints (open ze één voor één)

<details><summary>Hint 1: welk tool?</summary>

`dotnet-counters`: de pool is *gezond* (async waits houden geen threads vast); de latency komt van **wachten bij de semafoor**. Tel uitgaande calls per seconde: dat is gelijk aan het request-tempo.
</details>

<details><summary>Hint 2: waar?</summary>

De lock is een serialisatiepunt voor **elke** request, ook voor requests die om dezelfde valuta vragen die de vorige request net heeft opgehaald.
</details>

<details><summary>Hint 3: waarom?</summary>

Eén globale async lock maakt van een cachebare lookup een seriële pipeline: doorvoer = 1 ÷ (calltijd). De waarde is gedeeld en verandert traag: haal hem **één keer per key** op (met een gedeelde `Lazy<Task>`), laat iedereen op diezelfde task awaiten, en laat hem bewust verlopen/vernieuwen.
</details>
