# Lab 3: lekken & retentie

".NET heeft een garbage collector, dus er kan niets lekken": hier gaat die aanname onderuit, en leer je lezen *waarom* iets wat de GC had moeten opruimen nog steeds leeft.

**Skills:** GC-roots, dominators, retentiepaden, snapshot-diffing, 'groeiend' versus 'nog niet verzameld'.

**Meesterschapstoets:** Noem, alleen op basis van een snapshot-diff, de root die een gelekt object in leven houdt.

Draai er een: `dotnet run -c Release --project labs/L03-leaks/exercises/<id>` (verwacht `FAIL`), werk 'm uit zoals beschreven in de [top-level README](../../README.md), en open pas daarna de oplossing.

**Verder lezen:** [Leeslijst Lab 3](../../docs/READING-LIST.md#lab-3-leaks-retention)

## Exercises
| Exercise | Scenario | Spoiler |
|---|---|---|
| [L03-01-event-hub](exercises/L03-01-event-hub/README.md) | Event hub | [oplossing](solutions/L03-01-event-hub/SOLUTION.md) |
| [L03-02-session-cache](exercises/L03-02-session-cache/README.md) | Session cache | [oplossing](solutions/L03-02-session-cache/SOLUTION.md) |
| [L03-03-price-ticker](exercises/L03-03-price-ticker/README.md) | Price ticker | [oplossing](solutions/L03-03-price-ticker/SOLUTION.md) |
| [L03-04-batch-report](exercises/L03-04-batch-report/README.md) | Batch report | [oplossing](solutions/L03-04-batch-report/SOLUTION.md) |
| [L03-05-tag-registry](exercises/L03-05-tag-registry/README.md) | Tag registry | [oplossing](solutions/L03-05-tag-registry/SOLUTION.md) |
| [L03-06-callback-registry](exercises/L03-06-callback-registry/README.md) | Callback registry | [oplossing](solutions/L03-06-callback-registry/SOLUTION.md) |

## Eindbaas-gevecht
Een vermomde combinatie van de defecten uit dit lab: geen hints per defect. Doe deze als laatste, en schrijf daarna op uit welke exercise elk defect kwam.

| Exercise | Scenario | Spoiler |
|---|---|---|
| [L03-boss-session-gateway](exercises/L03-boss-session-gateway/README.md) | Session gateway | [oplossing](solutions/L03-boss-session-gateway/SOLUTION.md) |
