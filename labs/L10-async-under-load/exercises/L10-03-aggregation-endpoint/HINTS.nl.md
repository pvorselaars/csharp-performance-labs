# Hints (open ze één voor één)

<details><summary>Hint 1: welk tool?</summary>

Er is geen CPU-probleem. Kijk naar **concurrency**: de `peakInflight`-metric (in het echt het eigen dashboard van de downstream, of je uitgaande `HttpClient`-metrics).
</details>

<details><summary>Hint 2: waar?</summary>

Elke request waaiert 30 breed uit. Hoeveel requests zijn er tegelijk onderweg, en wat is het product?
</details>

<details><summary>Hint 3: waarom?</summary>

Parallellisme per request vermenigvuldigt met request-concurrency: 16 gebruikers × 30 calls = 480 tegelijk onderweg. Een limiet *per request* begrenst het totaal niet. Leg de grens waar de gedeelde resource zit: een semafoor (of een beperkte `HttpClient`-handler / bulkhead) **gedeeld door alle requests**.
</details>
