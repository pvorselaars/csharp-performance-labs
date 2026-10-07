Revision bloat

*Growth Spurt*

## Symptom
A job overwrites ten live gauge documents sixty times. Nobody reads old readings, yet the database grows with every run. The harness reports how many versions of a gauge are stored (`revisionsPerDoc`).

## Goal
Same result (checksum), and:

| Budget | Value |
|---|---|
| revisionsPerDoc | ≤ 5 |

> The RavenDB server runs inside the harness process, on loopback, seeded once. On a real network each round trip would cost far more than it does here.
