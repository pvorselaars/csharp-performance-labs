# Hints (open ze één voor één)

<details><summary>Hint 1: welke tool?</summary>

Downstream-calls per binnenkomende request (`downstreamCalls` ÷ 400), het aantal requests dat opgeeft (`giveUps`), en de totale latency: een retry storm die concurreert om de beperkte workers van de downstream is end-to-end trager, niet alleen luidruchtiger.
</details>

<details><summary>Hint 2: waar?</summary>

Wanneer de downstream overbelast is en snel faalt, wat doet een onmiddellijke retry dan met het aantal calls in flight?
</details>

<details><summary>Hint 3: waarom?</summary>

Retries zijn extra load, verstuurd precies op het moment dat de dependency het zwakst is: een **retry storm**. De remedie is overbelasting voorkomen (een **bulkhead/concurrency limit**), spaarzaam retryen met **backoff + jitter** en een **retry budget**, en snel falen (circuit breaker) in plaats van opstapelen.
</details>
