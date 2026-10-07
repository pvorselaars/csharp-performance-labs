Wait every time

*Slow Import*

## Symptom
An import job stores 100 tickets, then reports how many are open, and the count must be correct. The job spends most of its time waiting. The harness counts round trips (`requestsPerOp`).

## Goal
Same result (checksum), and:

| Budget | Value |
|---|---|
| Median time | 60 ref-ms |
| requestsPerOp | ≤ 6 |

> The RavenDB server runs inside the harness process, on loopback, seeded once. On a real network each round trip would cost far more than it does here.
