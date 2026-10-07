# Hints (open ze één voor één)

<details><summary>Hint 1: welke tool?</summary>

Verbindingen gezien door de callee (`connections`), of `ss -tan | grep -c TIME-WAIT`; dotTrace: handler- en socketconstructie per request.
</details>

<details><summary>Hint 2: waar?</summary>

Welk object maakt elke `new HttpClient()` onderliggend aan, en wat bezit het?
</details>

<details><summary>Hint 3: waarom?</summary>

Een client bezit een handler met een **connection pool**; een nieuwe client is een nieuwe pool en een nieuwe verbinding. Gebruik `IHttpClientFactory` (typed/named clients) of een langlevende client met `PooledConnectionLifetime` zodat verbindingen hergebruikt worden en DNS-wijzigingen toch gevolgd worden.
</details>
