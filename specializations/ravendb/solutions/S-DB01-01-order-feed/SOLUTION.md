# Solution

**Root cause:** the loop calls `session.Load<Customer>` per order. Each customer is a distinct document, so the session cache never helps: 1 query + 100 loads = 101 round trips (N+1).

**Fix:** `session.Query<Order>().Include(o => o.CustomerId)`. The server returns the customers with the orders, and `Load` is then served from the session cache: 1 round trip.

**Extra credit:** `Include` with a projection; `Load(string[] ids)` for the multi-get form; the `MaxNumberOfRequestsPerSession` guard (30) that the harness lifts so the problem is measurable.
