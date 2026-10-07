# Solution

**Root cause:** the filter is on a property of a related document, so the client pulls every order (plus the customers via `Include`) and joins in memory.

**Fix:** a static index (`Orders_ByCountry`) whose map reads the customer with `LoadDocument<Customer>(o.CustomerId).Country`. The index is queried with `Where(r => r.Country == "NL").OfType<Order>()`, and only the matching orders are returned.

**Extra credit:** what happens to the index when a customer's country changes (RavenDB re-indexes the referencing orders); the cost of `LoadDocument` on index time; denormalising the country into the order.
