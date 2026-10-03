# Solution

**Root cause:** the total is a derived value, so the only way the query can filter on it is to download every invoice and sum its lines on the client, on every run.

**Fix:** a static index (`Invoices_ByTotal`) whose map computes `Total = Lines.Sum(...)`. The server evaluates the sum once per write, and the query `Where(r => r.Total > 1740).OfType<Invoice>()` returns only the matching invoices.

**Extra credit:** store the total in the document at write time (denormalise) and compare the trade-offs; project the invoice number instead of `OfType`; sort by the computed total.
