# Solution

**Root cause:** `Revisions.Get<T>(id, asOf)` is a remote call per document, so 100 contracts cost 100 round trips.

**Fix:** `session.Load<Contract>(ids, b => b.IncludeRevisions(asOf))` loads the documents and the revisions as of that date in one request and puts the revisions in the session; the following `Revisions.Get<T>(id, asOf)` calls are answered from the session cache.

**Extra credit:** `IncludeRevisions` on a query; including a revision by change vector; the revisions-bin for deleted documents.
