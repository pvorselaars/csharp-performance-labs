# Lab 2: allocaties & GC-druk

De code is correct en niet overduidelijk traag. Hij laat alleen de garbage collector overuren maken. Hier leer je dat te zien voordat het als latency naar buiten komt.

**Skills:** Allocatie call stacks, gen0/1/2-gedrag, boxing, spans, pooling, finalizers, LOH.

**Mastery checkpoint:** Leg uit waarom 1 GB kortlevende rommel goedkoper kan zijn dan 50 MB langlevende objecten.

Draai er een: `dotnet run -c Release --project labs/L02-allocations/exercises/<id>` (verwacht `FAIL`), werk hem uit zoals beschreven in de [top-level README](../../README.md), en open pas daarna de oplossing.

**Meer leesvoer:** [Lab 2 leeslijst](../../docs/READING-LIST.md#lab-2-allocations-gc-pressure)

## Exercises
| Exercise | Scenario | Spoiler |
|---|---|---|
| [L02-01-sensor-metrics](exercises/L02-01-sensor-metrics/README.md) | Sensormetingen | [oplossing](solutions/L02-01-sensor-metrics/SOLUTION.md) |
| [L02-02-order-lines](exercises/L02-02-order-lines/README.md) | Orderregels | [oplossing](solutions/L02-02-order-lines/SOLUTION.md) |
| [L02-03-page-renderer](exercises/L02-03-page-renderer/README.md) | Pagina-renderer | [oplossing](solutions/L02-03-page-renderer/SOLUTION.md) |
| [L02-04-tile-cache](exercises/L02-04-tile-cache/README.md) | Tile-cache | [oplossing](solutions/L02-04-tile-cache/SOLUTION.md) |
| [L02-05-price-lookup](exercises/L02-05-price-lookup/README.md) | Prijsopzoeking | [oplossing](solutions/L02-05-price-lookup/SOLUTION.md) |
| [L02-06-log-classifier-2](exercises/L02-06-log-classifier-2/README.md) | Logclassificatie, deel 2 | [oplossing](solutions/L02-06-log-classifier-2/SOLUTION.md) |
| [L02-07-route-stats](exercises/L02-07-route-stats/README.md) | Routestatistieken | [oplossing](solutions/L02-07-route-stats/SOLUTION.md) |
| [L02-08-small-sums](exercises/L02-08-small-sums/README.md) | Kleine sommen | [oplossing](solutions/L02-08-small-sums/SOLUTION.md) |
| [L02-09-case-lookup](exercises/L02-09-case-lookup/README.md) | Case-opzoeking | [oplossing](solutions/L02-09-case-lookup/SOLUTION.md) |

## Eindbaas-gevecht
Een vermomde combinatie van de defecten uit dit lab: geen hints per defect. Doe deze als laatste, en noteer daarna van welke exercise elk defect afkomstig was.

| Exercise | Scenario | Spoiler |
|---|---|---|
| [L02-boss-shipment-manifest](exercises/L02-boss-shipment-manifest/README.md) | Verzendmanifest | [oplossing](solutions/L02-boss-shipment-manifest/SOLUTION.md) |
