# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

dotMemory-allocaties: kijk per request naar de `string`s en de internals van `Dictionary<int, decimal>`, en naar wat ze toewijst. dotTrace: welke constructor duikt op bij elke request, en hoe vaak draait hij per 1.500 requests?
</details>

<details><summary>Hint 2: waar?</summary>

Tel hoe vaak `PriceCatalog` wordt geconstrueerd. Wie maakt hem aan? Kijk hoe de services geregistreerd zijn in het `builder.Services`-blok.
</details>

<details><summary>Hint 3: waarom?</summary>

Een *transient* service krijgt elke keer dat hij wordt opgelost een nieuwe instantie, en alles waarvan hij afhangt dat ook transient is, idem. `PricingService` wordt per request opgelost, dus de prijslijst wordt per request geparst. Dure, alleen-lezen referentiedata hoort in een **singleton**.
</details>
