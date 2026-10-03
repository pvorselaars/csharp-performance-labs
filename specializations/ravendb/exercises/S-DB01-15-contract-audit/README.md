As-of audit

*Audit Trail*

## Symptom
An audit reads the state of 100 contracts as they were at a past date; the contracts have been revised since. It takes far longer than reading 100 documents should. The harness counts round trips (`requestsPerOp`).

## Goal
Same result (checksum), and:

| Budget | Value |
|---|---|
| requestsPerOp | ≤ 2 |

> The RavenDB server runs inside the harness process, on loopback, seeded once. On a real network each round trip would cost far more than it does here.
