One by one

*Rush Hour*

## Symptom
An ingest job writes 1 000 sensor readings. It takes seconds, though the server is nearly idle. The harness counts round trips (`requestsPerOp`).

## Goal
Same result (checksum), and:

| Budget | Value |
|---|---|
| Median time | 120 ref-ms |
| requestsPerOp | ≤ 5 |

> The RavenDB server runs inside the harness process, on loopback, seeded once. On a real network each round trip would cost far more than it does here.
