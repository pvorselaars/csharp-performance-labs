# Lab 5: de library-stack, één aanroeper

De bug zit meestal niet in jouw code. Hij zit in *hoe je* EF Core, HttpClient, JSON of het bestandssysteem *gebruikt*, en je moet hem opzoeken in de subsysteem-view van iemand anders, niet die van jou.

**Vaardigheden:** EF Core, HttpClient, JSON, logging, file I/O: vind elk ervan vanuit de subsysteem-view.

**Mastery-checkpoint:** Vind een N+1 puur vanuit de SQL-view, voordat je ook maar één regel LINQ leest.

Draai er een: `dotnet run -c Release --project labs/L05-library-stack/exercises/<id>` (verwacht `FAIL`), werk hem uit zoals beschreven in de [top-level README](../../README.md), en open pas daarna de oplossing.

**Verder lezen:** [Lab 5 leeslijst](../../docs/READING-LIST.md#lab-5-library-stack-under-a-single-caller)

## Exercises
| Exercise | Scenario | Spoiler |
|---|---|---|
| [L05-01-customer-orders](exercises/L05-01-customer-orders/README.md) | Klantorders | [oplossing](solutions/L05-01-customer-orders/SOLUTION.md) |
| [L05-02-product-list](exercises/L05-02-product-list/README.md) | Productlijst | [oplossing](solutions/L05-02-product-list/SOLUTION.md) |
| [L05-03-sku-totals](exercises/L05-03-sku-totals/README.md) | SKU-totalen | [oplossing](solutions/L05-03-sku-totals/SOLUTION.md) |
| [L05-04-service-calls](exercises/L05-04-service-calls/README.md) | Service calls | [oplossing](solutions/L05-04-service-calls/SOLUTION.md) |
| [L05-05-json-response](exercises/L05-05-json-response/README.md) | JSON-response | [oplossing](solutions/L05-05-json-response/SOLUTION.md) |
| [L05-06-debug-logging](exercises/L05-06-debug-logging/README.md) | Debug-logging | [oplossing](solutions/L05-06-debug-logging/SOLUTION.md) |
| [L05-07-audit-log](exercises/L05-07-audit-log/README.md) | Audit-log | [oplossing](solutions/L05-07-audit-log/SOLUTION.md) |
| [L05-08-byte-reader](exercises/L05-08-byte-reader/README.md) | Byte-reader | [oplossing](solutions/L05-08-byte-reader/SOLUTION.md) |

## Eindbaas-gevecht
Een vermomde combinatie van de defecten uit dit lab: geen hints per defect. Doe deze als laatste, en schrijf daarna op uit welke exercise elk defect kwam.

| Exercise | Scenario | Spoiler |
|---|---|---|
| [L05-boss-order-report](exercises/L05-boss-order-report/README.md) | Orderrapport | [oplossing](solutions/L05-boss-order-report/SOLUTION.md) |
