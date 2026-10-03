# Solution

**Root cause:** the client re-reads and re-aggregates every entry on every request. Streaming removes the memory spike but not the O(entries) work.

**Fix:** a map-reduce index (`Entries_ByCustomer`) that sums `Cents` per `CustomerId`. The server maintains it as entries change; the query reads 100 precomputed rows.

**Extra credit:** time-bucketed map-reduce (per customer per month); `OutputReduceToCollection`; what happens to query results while the index is stale (`WaitForNonStaleResults`).
