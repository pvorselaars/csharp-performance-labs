# Lab 1: voor de hand liggende hot spots

De makkelijke winst: code die gewoonweg meer werk doet dan nodig, op een manier die een profiler in één oogopslag duidelijk maakt zodra je weet waar je moet kijken.

**Vaardigheden:** Een call tree lezen; self vs. total time; sampling vs. tracing; budgets als regressie-gate.

**Mastery checkpoint:** Vind het top self-time-frame en zijn eerste "jouw code"-aanroeper in minder dan 5 minuten, en zeg of het probleem CPU, allocatie of aantal aanroepen is.

Draai er een: `dotnet run -c Release --project labs/L01-hot-spots/exercises/<id>` (verwacht `FAIL`), werk hem uit zoals beschreven in de [top-level README](../../README.md), en open pas daarna de oplossing.

**Meer lezen:** [Leeslijst Lab 1](../../docs/READING-LIST.md#lab-1-obvious-hot-spots)

## Exercises
| Exercise | Scenario | Spoiler |
|---|---|---|
| [L01-01-invoice-export](exercises/L01-01-invoice-export/README.md) | Factuurexport | [oplossing](solutions/L01-01-invoice-export/SOLUTION.md) |
| [L01-02-contact-import](exercises/L01-02-contact-import/README.md) | Contactimport | [oplossing](solutions/L01-02-contact-import/SOLUTION.md) |
| [L01-03-log-classifier](exercises/L01-03-log-classifier/README.md) | Logclassificatie | [oplossing](solutions/L01-03-log-classifier/SOLUTION.md) |
| [L01-04-quantity-parsing](exercises/L01-04-quantity-parsing/README.md) | Hoeveelheden parsen | [oplossing](solutions/L01-04-quantity-parsing/SOLUTION.md) |
| [L01-05-customer-dashboard](exercises/L01-05-customer-dashboard/README.md) | Klantdashboard | [oplossing](solutions/L01-05-customer-dashboard/SOLUTION.md) |
| [L01-06-leaderboard](exercises/L01-06-leaderboard/README.md) | Scorebord | [oplossing](solutions/L01-06-leaderboard/SOLUTION.md) |
| [L01-07-field-reader](exercises/L01-07-field-reader/README.md) | Veldlezer | [oplossing](solutions/L01-07-field-reader/SOLUTION.md) |

## Eindbaas-gevecht
Een vermomde combinatie van de defecten uit dit lab: geen hints per defect. Doe deze als laatste, en schrijf daarna op uit welke exercise elk defect kwam.

| Exercise | Scenario | Spoiler |
|---|---|---|
| [L01-boss-order-ledger](exercises/L01-boss-order-ledger/README.md) | Orderboek | [oplossing](solutions/L01-boss-order-ledger/SOLUTION.md) |
