Ledger totals

*Month End*

## Symptom
A finance page shows the total per customer over 12 000 ledger entries (100 numbers on screen). Every refresh takes as long as the first, and the time grows with the size of the ledger.

## Goal
Same result (checksum), and:

| Budget | Value |
|---|---|
| Median time | 10 ref-ms |
| Median allocated | 2 MB |
| requestsPerOp | ≤ 2 |

> The RavenDB server runs inside the harness process, on loopback, seeded once. On a real network each round trip would cost far more than it does here.
