# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

EF command logging (`LogTo`), de SQL-view, of OpenTelemetry-traces: tel statements *per request*. De harness drukt het totaal van de run af.
</details>

<details><summary>Hint 2: waar?</summary>

Deel `sqlCommands` door het aantal requests. Welk getal krijg je, en wat in de handler produceert dat?
</details>

<details><summary>Hint 3: waarom?</summary>

Weer N+1 (L05-01), maar nu betaalt elke request ervoor en vermenigvuldigt concurrency de databasebelasting. Eén aggregerende query geeft hetzelfde antwoord.
</details>
