# Solution

**Root cause:** `Query<Invoice>().ToList()` downloads and deserialises every full invoice (30 lines each), and the session then tracks all 1500 of them, to read one `int` each.

**Fix:** project on the server: `.Select(i => new InvoiceTotal { Total = i.Total })`. Only the one field crosses the wire, and projections are not tracked by the session.

**Extra credit:** `ProjectInto<T>()`; a static index that stores `Total` so the projection reads from the index instead of the document; `session.Advanced.Stream`.
