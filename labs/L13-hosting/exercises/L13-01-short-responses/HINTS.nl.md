# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

Tel het aantal losse connecties (`connections`), `ss -tan | grep -c TIME-WAIT`, en response headers (`curl -v`).
</details>

<details><summary>Hint 2: waar?</summary>

Welke header bepaalt of de client de connectie mag hergebruiken, en wie zet die?
</details>

<details><summary>Hint 3: waarom?</summary>

HTTP/1.1-connecties zijn standaard persistent. Een `Connection: close`-header (toegevoegd door middleware, een proxy of een load balancer-instelling) dwingt één request per connectie af. Handshakes en slow-start domineren bij kleine responses. Verwijder de header (en controleer wat er vóór je staat: LB-idle-timeouts moeten *langer* zijn dan Kestrel's keep-alive).
</details>
