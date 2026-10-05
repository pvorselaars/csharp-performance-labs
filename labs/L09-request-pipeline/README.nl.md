# Lab 9: de request pipeline (ASP.NET Core)

Grotendeels dezelfde lessen als Labs 1–2, behalve dat er nu een webserver en een request tussen elke meting in zitten, en dat verandert wat "kosten" eigenlijk betekent.

**Vaardigheden:** Kosten per request: middleware, DI, bodies schrijven en lezen. Bytes per request meten.

**Mastery checkpoint:** De toegewezen bytes per request van een naïef endpoint 10× verlagen en kunnen zeggen welke middleware wat kostte.

Draai er een: `dotnet run -c Release --project labs/L09-request-pipeline/exercises/<id>` (verwacht `FAIL`), werk hem uit zoals beschreven in de [top-level README](../../README.nl.md), en open pas daarna de oplossing.

**Verder lezen:** [Leeslijst Lab 9](../../docs/READING-LIST.nl.md#lab-9-the-request-pipeline)

## Exercises
| Exercise | Scenario | Spoiler |
|---|---|---|
| [L09-01-hello-endpoint](exercises/L09-01-hello-endpoint/README.nl.md) | Hello endpoint | [oplossing](solutions/L09-01-hello-endpoint/SOLUTION.nl.md) |
| [L09-02-audit-middleware](exercises/L09-02-audit-middleware/README.nl.md) | Audit middleware | [oplossing](solutions/L09-02-audit-middleware/SOLUTION.nl.md) |
| [L09-03-report-endpoint](exercises/L09-03-report-endpoint/README.nl.md) | Report endpoint | [oplossing](solutions/L09-03-report-endpoint/SOLUTION.nl.md) |
| [L09-04-price-endpoint](exercises/L09-04-price-endpoint/README.nl.md) | Price endpoint | [oplossing](solutions/L09-04-price-endpoint/SOLUTION.nl.md) |
| [L09-05-import-endpoint](exercises/L09-05-import-endpoint/README.nl.md) | Import endpoint | [oplossing](solutions/L09-05-import-endpoint/SOLUTION.nl.md) |

## Eindbaas-gevecht
Een vermomde combinatie van de defecten uit dit lab: geen hints per defect. Doe deze als laatste, en schrijf daarna op uit welke exercise elk defect kwam.

| Exercise | Scenario | Spoiler |
|---|---|---|
| [L09-boss-storefront-checkout](exercises/L09-boss-storefront-checkout/README.nl.md) | Storefront checkout (eindbaas van L9) | [oplossing](solutions/L09-boss-storefront-checkout/SOLUTION.nl.md) |
