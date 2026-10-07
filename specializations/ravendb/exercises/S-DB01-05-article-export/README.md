Deep pages

*Long Night*

## Symptom
A nightly export of all 6 000 articles takes far longer than copying the data should. The harness counts round trips (`requestsPerOp`).

## Goal
Same result (checksum), and:

| Budget | Value |
|---|---|
| requestsPerOp | ≤ 2 |

> The RavenDB server runs inside the harness process, on loopback, seeded once. On a real network each round trip would cost far more than it does here.
