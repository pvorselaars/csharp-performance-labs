# Solution

**Root cause:** `Load` pulls 1 000 four-kilobyte documents to the client, the session tracks them, and change detection writes them back, all to set one `int` per note.

**Fix:** queue a server-side patch per note with `session.Advanced.Defer(new PatchCommandData(id, null, new PatchRequest { Script = "this.Stamp = args.stamp;", ... }, null))` and call `SaveChanges()` once. The patches run on the server inside one transaction: one round trip, nothing downloaded.

**Extra credit:** `store.Operations.Send(new PatchByQueryOperation(...))` to patch by query without even knowing the ids; `session.Advanced.Patch` for a single document; what a patch cannot do (it cannot read other documents).
