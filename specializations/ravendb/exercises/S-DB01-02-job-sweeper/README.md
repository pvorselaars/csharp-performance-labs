Growing session

*Overtime Shift*

## Symptom
A worker marks 400 jobs as picked up. It takes far longer than 400 small writes should, and the time grows faster than the number of jobs. The harness counts round trips (`requestsPerOp`) and allocation.

## Goal
Same result (checksum), and:

| Budget | Value |
|---|---|
| Median time | 100 ref-ms |
| Median allocated | 12 MB |
| requestsPerOp | ≤ 3 |

> The RavenDB server runs inside the harness process, on loopback, seeded once. On a real network each round trip would cost far more than it does here.
