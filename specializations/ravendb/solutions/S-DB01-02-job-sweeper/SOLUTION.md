# Solution

**Root cause:** `SaveChanges()` inside the loop. The session tracks all 400 jobs, so each call compares every tracked entity against its original snapshot (change detection is O(tracked)) and then sends its own write request: 400 saves x 400 tracked entities, and 401 round trips.

**Fix:** make all the edits and call `SaveChanges()` once. RavenDB sends all changes as a single transactional batch: 2 round trips.

**Extra credit:** `session.Advanced.Patch` to avoid loading documents just to edit one field; `session.Advanced.Evict` and `Clear` to cap what a long session tracks; `BulkInsert` when you are only writing.
