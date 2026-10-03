# S-DB01-01 - Order feed

*Feed Me*

## Symptom
A feed endpoint returns the latest 100 orders, each weighted by its customer's tier. Locally it feels fine; against a server a network hop away it crawls. The harness counts the client's round trips to the server (`requestsPerOp`).

## Goal
Same result (checksum), and:

| Budget | Value |
|---|---|
| Median time | 10 ref-ms |
| requestsPerOp | 1 |

> The RavenDB server runs inside the harness process, on loopback. On a real network each round trip would cost far more than it does here.
