# Lab 10: async, threads & de pool onder belasting

Opnieuw de lessen van Lab 4, maar nu met echte gelijktijdige belasting op de thread pool in plaats van één aanroep tegelijk — en dat is precies het moment waarop die problemen daadwerkelijk toeslaan.

**Vaardigheden:** Starvation, blocking, fan-out, cancellation, achtergrondwerk.

**Mastery checkpoint:** Voorspel vanuit een latency-vs-concurrency-grafiek en de pool-counters de bug, voordat je de code opent.

Draai er één: `dotnet run -c Release --project labs/L10-async-under-load/exercises/<id>` (verwacht `FAIL`), werk hem uit zoals beschreven in de [top-level README](../../README.md), en open pas daarna de oplossing.

**Verder lezen:** [Leeslijst Lab 10](../../docs/READING-LIST.md#lab-10-async-threads-the-pool-under-load)

## Exercises
| Exercise | Scenario | Spoiler |
|---|---|---|
| [L10-01-database-endpoint](exercises/L10-01-database-endpoint/README.md) | Database-endpoint | [oplossing](solutions/L10-01-database-endpoint/SOLUTION.md) |
| [L10-02-async-handler](exercises/L10-02-async-handler/README.md) | Async handler | [oplossing](solutions/L10-02-async-handler/SOLUTION.md) |
| [L10-03-aggregation-endpoint](exercises/L10-03-aggregation-endpoint/README.md) | Aggregatie-endpoint | [oplossing](solutions/L10-03-aggregation-endpoint/SOLUTION.md) |
| [L10-04-currency-conversion](exercises/L10-04-currency-conversion/README.md) | Valuta-conversie | [oplossing](solutions/L10-04-currency-conversion/SOLUTION.md) |
| [L10-05-impatient-clients](exercises/L10-05-impatient-clients/README.md) | Ongeduldige clients | [oplossing](solutions/L10-05-impatient-clients/SOLUTION.md) |
| [L10-06-background-jobs](exercises/L10-06-background-jobs/README.md) | Achtergrondtaken | [oplossing](solutions/L10-06-background-jobs/SOLUTION.md) |

## Eindbaas-gevecht
Een vermomde combinatie van de defecten uit dit lab: geen hints per defect. Doe deze als laatste, en schrijf daarna op uit welke exercise elk defect afkomstig was.

| Exercise | Scenario | Spoiler |
|---|---|---|
| [L10-boss-async-quotes](exercises/L10-boss-async-quotes/README.md) | Async quotes (eindbaas van Lab 10) | [oplossing](solutions/L10-boss-async-quotes/SOLUTION.md) |
