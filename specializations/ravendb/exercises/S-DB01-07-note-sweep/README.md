Patch flag

*Night Shift*

## Symptom
A nightly sweep stamps 1 000 notes as reviewed. The notes are large (4 KB each). The job allocates and transfers far more than seems proportionate to what it changes. The harness counts round trips (`requestsPerOp`) and allocation.

## Goal
Same result (checksum), and:

| Budget | Value |
|---|---|
| Median time | 150 ref-ms |
| Median allocated | 12 MB |
| requestsPerOp | ≤ 1 |

> The RavenDB server runs inside the harness process, on loopback, seeded once. On a real network each round trip would cost far more than it does here.
