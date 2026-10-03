Facet counts

*Brand Loyalty*

## Symptom
The category sidebar shows how many products each brand has in the category: eight numbers. Building it is slow and allocation-heavy.

## Goal
Same result (checksum), and:

| Budget | Value |
|---|---|
| Median time | 6 ref-ms |
| Median allocated | 1 MB |
| requestsPerOp | ≤ 1 |

> The RavenDB server runs inside the harness process, on loopback, seeded once. On a real network each round trip would cost far more than it does here.
