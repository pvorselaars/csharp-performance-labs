# Lab 11: data access onder belasting

Waar de database het knelpunt wordt, en je dat moet herkennen aan query-aantallen en timings, niet door de LINQ te lezen en te raden welke SQL eruit komt.

**Vaardigheden:** N+1, row explosion, connections te lang vastgehouden, DbContext-levensduur.

**Mastery checkpoint:** Noem de SQL-statement met de hoogste *totale* tijd (aantal × duur).

Draai er één: `dotnet run -c Release --project labs/L11-data-access/exercises/<id>` (verwacht `FAIL`), werk het uit zoals beschreven in de [README op het hoogste niveau](../../README.md), en open pas daarna de oplossing.

**Verder lezen:** [Leeslijst Lab 11](../../docs/READING-LIST.md#lab-11-data-access-under-load)

## Exercises
| Exercise | Scenario | Spoiler |
|---|---|---|
| [L11-01-orders-dashboard](exercises/L11-01-orders-dashboard/README.md) | Orders-dashboard | [oplossing](solutions/L11-01-orders-dashboard/SOLUTION.md) |
| [L11-02-customer-profile](exercises/L11-02-customer-profile/README.md) | Klantprofiel | [oplossing](solutions/L11-02-customer-profile/SOLUTION.md) |
| [L11-03-queued-requests](exercises/L11-03-queued-requests/README.md) | Idle database, requests in de wachtrij | [oplossing](solutions/L11-03-queued-requests/SOLUTION.md) |
| [L11-04-paged-catalogue](exercises/L11-04-paged-catalogue/README.md) | Catalogus met paginering | [oplossing](solutions/L11-04-paged-catalogue/SOLUTION.md) |

## Eindbaas-gevecht
Een vermomde combinatie van de defecten uit dit lab: geen hints per defect. Doe deze als laatste, en schrijf daarna op uit welke exercise elk defect afkomstig was.

| Exercise | Scenario | Spoiler |
|---|---|---|
| [L11-boss-orders-service](exercises/L11-boss-orders-service/README.md) | Orders-service (eindbaas van Lab 11) | [oplossing](solutions/L11-boss-orders-service/SOLUTION.md) |
