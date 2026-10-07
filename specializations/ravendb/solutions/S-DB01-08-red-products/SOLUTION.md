# Solution

**Root cause:** `Query<Product>().ToList()` downloads and deserialises all 5 000 products and the filter runs on the client.

**Fix:** let the server filter: `.Search(p => p.Tags, "red")`. The auto-index tokenises the field, so the query returns only the matching documents.

**Extra credit:** model tags as `List<string>` and use `Where(p => p.Tags.Contains("red"))` or `ContainsAny`; add a projection on top (see S-DB01-03); `Search` options (`SearchOperator`, boosting).
