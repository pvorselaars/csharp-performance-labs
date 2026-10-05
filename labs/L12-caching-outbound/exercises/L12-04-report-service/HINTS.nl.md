# Hints (open ze één voor één)

<details><summary>Hint 1: welke tool?</summary>

Tel hoeveel *losse* responses er zijn versus hoeveel requests: 20 vs 1.200. Die verhouding is het potentieel van de cache.
</details>

<details><summary>Hint 2: waar?</summary>

Welke laag zou identieke requests kunnen beantwoorden zonder de handler te draaien?
</details>

<details><summary>Hint 3: waarom?</summary>

ASP.NET Core's **output caching**-middleware slaat hele responses op, geïndexeerd op de request, en bedient herhalingen zonder het endpoint aan te roepen. Het bundelt ook gelijktijdige identieke requests (resource locking), zodat de handler maar één keer per key per expiry draait.
</details>
