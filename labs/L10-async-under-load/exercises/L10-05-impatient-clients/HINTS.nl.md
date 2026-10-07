# Hints (open ze één voor één)

<details><summary>Hint 1: welk tool?</summary>

Server-side counters van *actieve requests* blijven hoog nadat clients weg zijn (de `peakInFlight` van de harness), en de latency van de geduldige clients ligt ver boven de 200 ms die het werk kost. Log of tel werk dat uitgevoerd wordt nadat `HttpContext.RequestAborted` is geannuleerd (`stepsAfterAbort`).
</details>

<details><summary>Hint 2: waar?</summary>

Waar komt de handler te weten dat de client weg is? Observeert een van zijn awaits dat, inclusief degene die op een vrij slot wacht?
</details>

<details><summary>Hint 3: waarom?</summary>

ASP.NET Core signaleert disconnects via `HttpContext.RequestAborted`, maar alleen code die **de token doorgeeft** (of controleert) reageert erop. Awaiten zonder de token loopt gewoon door tot voltooiing. Rijg een `CancellationToken` door elke geawaite call (wachten op een slot, database, HTTP, delay) zodat de hele keten afwikkelt en elk slot teruggaat naar iemand die nog luistert.
</details>
