Index join

*Country Club*

## Symptom
A report totals the orders placed by customers in one country. The country is a property of the customer. The report is slow and allocation-heavy for how few orders count toward the total.

## Goal
Same result (checksum), and:

| Budget | Value |
|---|---|
| Median time | 35 ref-ms |
| Median allocated | 12 MB |
| requestsPerOp | ≤ 1 |

> The RavenDB server runs inside the harness process, on loopback, seeded once. On a real network each round trip would cost far more than it does here.
