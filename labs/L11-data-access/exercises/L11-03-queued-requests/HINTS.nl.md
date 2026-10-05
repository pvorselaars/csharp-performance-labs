# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

Pool-metrics (connections in gebruik versus max), of een teller van wachtenden op de pool. Latency valt uiteen in *wachten op een connection* en *hem gebruiken*.
</details>

<details><summary>Hint 2: waar?</summary>

Hoe lang houdt elke request een connection vast, en wat doet hij gedurende het grootste deel van die tijd?
</details>

<details><summary>Hint 3: waarom?</summary>

Little's law: pool-doorvoer = grootte ÷ vasthoudtijd. Met 8 connections die elk ~31 ms worden vastgehouden, ligt het plafond op ~258 req/s. Houd een connection alleen vast voor het werk dat hem nodig heeft; doe traag niet-database-werk voordat je hem leent of nadat je hem vrijgeeft.
</details>
