Top ten

*Hall Of Fame*

## Symptom
The leaderboard shows ten players, yet producing it costs time and memory in proportion to the whole player table (5 000 players).

## Goal
Same result (checksum), and:

| Budget | Value |
|---|---|
| Median time | 10 ref-ms |
| Median allocated | 2 MB |
| requestsPerOp | ≤ 1 |

> The RavenDB server runs inside the harness process, on loopback, seeded once. On a real network each round trip would cost far more than it does here.
