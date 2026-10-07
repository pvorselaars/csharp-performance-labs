# Hints (open ze één voor één)

<details><summary>Hint 1: welk tool?</summary>

Counters + timeline zoals in L10-01. Lees daarna de handler *regel voor regel* en markeer elke regel die niet `await`.
</details>

<details><summary>Hint 2: waar?</summary>

`async` beschrijft de methode, niet wat erin zit. Welke statement houdt een thread 15 ms lang vast?
</details>

<details><summary>Hint 3: waarom?</summary>

Een synchrone wait (`Thread.Sleep`, `File.ReadAllText`, een synchrone database-driver, `HttpClient.Send`) binnen een async handler houdt een pool-thread vast, net als `.Result`. `await Task.Yield()` erna helpt niet. Gebruik de async API voor dezelfde operatie.
</details>
