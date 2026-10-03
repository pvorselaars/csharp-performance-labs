# S-DB01-boss - Sales dashboard

*Regional Manager*

## Symptom
The regional dashboard lists each north-region customer with the lifetime total of their orders. It is slow under every measure at once: round trips, data downloaded, time and allocation.

## Goal
Same result (checksum), and:

| Budget | Value |
|---|---|
| Median time | 10 ref-ms |
| Median allocated | 2 MB |
| requestsPerOp | ≤ 3 |

> The RavenDB server runs inside the harness process, on loopback, seeded once. On a real network each round trip would cost far more than it does here.
