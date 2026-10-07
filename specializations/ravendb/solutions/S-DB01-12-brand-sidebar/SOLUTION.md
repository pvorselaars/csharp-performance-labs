# Solution

**Root cause:** `ToList()` of the whole category, then a client-side group-by, to produce eight counts.

**Fix:** a static index over `Brand` and `Category`, and a facet query: `.AggregateBy(f => f.ByField(p => p.Brand)).Execute()`. The server answers from the index and returns eight small rows.

**Extra credit:** range facets (price bands); facets that include the zero counts; combining a facet query with the actual product page via `Take`.
