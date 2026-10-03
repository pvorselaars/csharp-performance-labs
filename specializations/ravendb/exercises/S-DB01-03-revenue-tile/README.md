Fat totals

*Heavy Lifting*

## Symptom
A revenue tile shows the total of all invoices. It is slow and allocation-heavy for such a small answer. The invoices are large documents (30 lines each). The harness reports time and allocation.

## Goal
Same result (checksum), and:

| Budget | Value |
|---|---|
| Median time | 40 ref-ms |
| Median allocated | 14 MB |

> The RavenDB server runs inside the harness process, on loopback, seeded once. On a real network each round trip would cost far more than it does here.
