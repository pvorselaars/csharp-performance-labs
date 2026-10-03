# Solution

**Root cause:** `ToList()` before `OrderByDescending(...).Take(10)`: the LINQ sort and limit run on the client, after all 5 000 players were downloaded and materialised.

**Fix:** keep the ordering in the query: `.OrderByDescending(p => p.Points).Take(10).ToList()`. The auto-index sorts by `Points`, and only ten documents cross the wire.

**Extra credit:** a static index with a sorted field; paging beyond the first ten; `Customize(x => x.NoTracking())`.
