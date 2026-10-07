# Hints (open er één per keer)

<details><summary>Hint 1: welke tool?</summary>

Kijk naar de `connections`-metric van de harness, en daarna naar de *sockets*: `ss -tan` (Linux) toont een stapel `TIME-WAIT`-entries. Zoek in dotTrace naar frames voor het opzetten van verbindingen (`ConnectAsync`, handler-constructie).
</details>

<details><summary>Hint 2: waar?</summary>

Wat wordt er binnen de loop elke iteratie aangemaakt, en wat bezit dat object dat duur is?
</details>

<details><summary>Hint 3: waarom?</summary>

`HttpClient` wikkelt om een handler die de connection pool bezit. Een nieuwe client betekent een nieuwe pool betekent een nieuwe verbinding (en een volledige handshake) per request; als er veel worden gedisposed, blijven sockets hangen in `TIME_WAIT`, en op schaal raak je zonder poorten. Hergebruik één client (of `IHttpClientFactory`, die de levensduur van de handler voor je beheert).
</details>
