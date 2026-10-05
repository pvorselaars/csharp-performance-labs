# Lab 12: caching & outbound calls

Caches en outbound calls die prima werken bij weinig verkeer en omvallen zodra er twee dingen tegelijk gebeuren, wat, op echte schaal, altijd het geval is.

**Vaardigheden:** Stampedes, onbegrensde caches, HttpClient-lifetime, output caching, retry storms.

**Mastery checkpoint:** Leg uit wat er gebeurt bij 2x capaciteit en kies het mechanisme dat gracieus degradeert.

Draai er een: `dotnet run -c Release --project labs/L12-caching-outbound/exercises/<id>` (verwacht `FAIL`), werk hem uit zoals beschreven in de [top-level README](../../README.md), en open pas daarna de oplossing.

**Verder lezen:** [Leeslijst Lab 12](../../docs/READING-LIST.md#lab-12-caching-outbound-calls)

## Exercises
| Exercise | Scenario | Spoiler |
|---|---|---|
| [L12-01-downstream-call](exercises/L12-01-downstream-call/README.md) | Downstream call | [oplossing](solutions/L12-01-downstream-call/SOLUTION.md) |
| [L12-02-popular-items](exercises/L12-02-popular-items/README.md) | Populaire items | [oplossing](solutions/L12-02-popular-items/SOLUTION.md) |
| [L12-03-search-results](exercises/L12-03-search-results/README.md) | Zoekresultaten | [oplossing](solutions/L12-03-search-results/SOLUTION.md) |
| [L12-04-report-service](exercises/L12-04-report-service/README.md) | Report service | [oplossing](solutions/L12-04-report-service/SOLUTION.md) |
| [L12-05-flaky-downstream](exercises/L12-05-flaky-downstream/README.md) | Wispelturige downstream | [oplossing](solutions/L12-05-flaky-downstream/SOLUTION.md) |

## Eindbaas-gevecht
Een vermomde combinatie van de defecten uit dit lab: geen hints per defect. Doe deze als laatste, en noteer daarna uit welke exercise elk defect kwam.

| Exercise | Scenario | Spoiler |
|---|---|---|
| [L12-boss-catalog-service](exercises/L12-boss-catalog-service/README.md) | Catalog service (eindbaas van Lab 12) | [oplossing](solutions/L12-boss-catalog-service/SOLUTION.md) |
