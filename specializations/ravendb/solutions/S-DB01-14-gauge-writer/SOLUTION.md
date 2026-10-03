# Solution

**Root cause:** revisions are enabled for every collection with no limit (`MinimumRevisionsToKeep` unset), so each of the 60 overwrites of a gauge adds another stored version: 60 revisions per gauge, and the database grows with every write.

**Fix:** bound the history. `MinimumRevisionsToKeep = 5` keeps the last few versions and purges older ones as new revisions are created. For a collection that needs no history at all, configure that collection with `Disabled = true`.

**Extra credit:** `MinimumRevisionsAgeToKeep` (keep by age); per-collection configuration instead of `Default`; `PurgeOnDelete` and the revisions bin; the cost of revisions on a hot document vs. using counters or time series for high-churn values.
