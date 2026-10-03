# Solution

**Root cause:** a session and a `SaveChanges()` per reading: 1 000 round trips and 1 000 transactions, each paying a network hop and a disk flush.

**Fix:** `store.BulkInsert()`: documents are streamed to the server over a single connection and committed in large batches, with no change tracking on the client.

**Extra credit:** one session with one `SaveChanges` for modest batches (a single transaction, all-or-nothing, unlike bulk insert); `BulkInsert` options (`SkipOverwriteIfUnchanged`); `Patch` by query for updates.
