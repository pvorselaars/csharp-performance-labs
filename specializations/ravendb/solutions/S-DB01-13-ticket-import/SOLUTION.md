# Solution

**Root cause:** `WaitForNonStaleResults` after every single write. Each wait blocks until the index has processed that one write, so the job pays 100 index round trips (plus 100 writes) for one answer.

**Fix:** write the whole import in one `SaveChanges()` and ask once, with `WaitForNonStaleResults`, at the point where a consistent read is needed: 2 round trips, and the index processes the 100 changes as a single batch.

**Extra credit:** `session.Advanced.WaitForIndexesAfterSaveChanges()` to wait for the indexes as part of the save; when *not* to wait at all (eventually consistent reads are fine for dashboards); `Customize(x => x.WaitForNonStaleResults(timeout))`.
