# Lab 6: hardware & runtime-effecten

Hetzelfde algoritme, hetzelfde aantal allocaties, en toch is de ene versie vijf keer zo traag, puur door hoe de CPU en de JIT het daadwerkelijk uitvoeren — en dat is niet altijd wat de code op papier doet vermoeden.

**Vaardigheden:** Cache-locality, branch prediction, false sharing, struct-kopieën, dispatch, tiered JIT, SIMD.

**Mastery checkpoint:** Voorspel welke van twee implementaties sneller is voordat je meet, en heb gelijk om de juiste reden.

Draai er een: `dotnet run -c Release --project labs/L06-hardware-runtime/exercises/<id>` (verwacht `FAIL`), werk hem uit zoals beschreven in de [top-level README](../../README.md), en open pas daarna de oplossing.

**Verder lezen:** [Leeslijst Lab 6](../../docs/READING-LIST.md#lab-6-hardware-runtime-effecten)

## Exercises
| Exercise | Scenario | Spoiler |
|---|---|---|
| [L06-01-matrix-walk](exercises/L06-01-matrix-walk/README.md) | Matrix-doorloop | [oplossing](solutions/L06-01-matrix-walk/SOLUTION.md) |
| [L06-02-particle-sweep](exercises/L06-02-particle-sweep/README.md) | Deeltjes-sweep | [oplossing](solutions/L06-02-particle-sweep/SOLUTION.md) |
| [L06-03-threshold-count](exercises/L06-03-threshold-count/README.md) | Drempel-telling | [oplossing](solutions/L06-03-threshold-count/SOLUTION.md) |
| [L06-04-worker-counters](exercises/L06-04-worker-counters/README.md) | Worker-tellers | [oplossing](solutions/L06-04-worker-counters/SOLUTION.md) |
| [L06-05-transform-batch](exercises/L06-05-transform-batch/README.md) | Batch-transformatie | [oplossing](solutions/L06-05-transform-batch/SOLUTION.md) |
| [L06-06-shape-areas](exercises/L06-06-shape-areas/README.md) | Oppervlaktes van vormen | [oplossing](solutions/L06-06-shape-areas/SOLUTION.md) |
| [L06-07-warmup-curve](exercises/L06-07-warmup-curve/README.md) | Warm-up-curve (een verkenning, geen pass/fail-gate) | [oplossing](solutions/L06-07-warmup-curve/SOLUTION.md) |
| [L06-08-count-matches](exercises/L06-08-count-matches/README.md) | Matches tellen | [oplossing](solutions/L06-08-count-matches/SOLUTION.md) |
| [L06-09-bit-counts](exercises/L06-09-bit-counts/README.md) | Bit-tellingen | [oplossing](solutions/L06-09-bit-counts/SOLUTION.md) |

## Eindbaas-gevecht
Een vermomde combinatie van de defecten uit dit lab: geen hints per defect. Doe deze als laatste, en schrijf daarna op welk defect uit welke exercise kwam.

| Exercise | Scenario | Spoiler |
|---|---|---|
| [L06-boss-sensor-grid](exercises/L06-boss-sensor-grid/README.md) | Sensor-grid | [oplossing](solutions/L06-boss-sensor-grid/SOLUTION.md) |
