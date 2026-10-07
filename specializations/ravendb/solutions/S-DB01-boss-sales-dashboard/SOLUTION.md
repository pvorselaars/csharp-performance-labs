# Solution

**Root causes (three):** (1) N+1: one orders query per north customer; (2) over-fetching: whole order documents (20 lines each) downloaded to read `Total`; (3) re-aggregation: the sums are recomputed from raw documents on every refresh.

**Fix:** a map-reduce index `Orders_ByCustomer` (sum of `Total` per `CustomerId`), queried once with `CustomerId.In(ids)`. Requests drop from 26 to 2, only 25 small rows travel, and the server maintains the totals incrementally.

**Extra credit:** fold the customer query into the index with `LoadDocument` and project name + total in one request; compare with `Include`.
