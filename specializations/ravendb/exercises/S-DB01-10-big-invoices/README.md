Computed field

*Big Spenders*

## Symptom
A report lists the invoices whose total is above a threshold (about one in ten qualify). An invoice's total is the sum of its lines. The report is slow and allocation-heavy for how few invoices it shows.

## Goal
Same result (checksum), and:

| Budget | Value |
|---|---|
| Median time | 40 ref-ms |
| Median allocated | 12 MB |
| requestsPerOp | ≤ 1 |

> The RavenDB server runs inside the harness process, on loopback, seeded once. On a real network each round trip would cost far more than it does here.
