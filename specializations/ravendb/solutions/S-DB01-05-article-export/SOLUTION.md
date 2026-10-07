# Solution

**Root cause:** `Skip/Take` paging for a full export: 61 requests, and each page makes the server walk past all earlier results again.

**Fix:** `session.Advanced.Stream(query)`: one request, results arrive as a forward-only stream and are never tracked or held all at once.

**Extra credit:** keep paging for user-facing pages (bounded `Take`), but use `Stream` for exports; `Take` on streams; why `Skip` on a large offset is O(offset).
