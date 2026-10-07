# Hints (open ze één voor één)

<details><summary>Hint 1: welke tool?</summary>

Tel loader-aanroepen versus losse keys (`loads`), en backend-load over tijd na een cache-flush.
</details>

<details><summary>Hint 2: waar?</summary>

`GetOrCreateAsync` checkt de cache, en voert bij een miss de factory uit en slaat het resultaat op. Wat gebeurt er als 8 requests op hetzelfde moment checken?
</details>

<details><summary>Hint 3: waarom?</summary>

`IMemoryCache.GetOrCreate*` is **niet atomair**: gelijktijdige misses draaien elk de factory (dit is L04-04 op requestschaal). Je hebt single-flight per key nodig: één gedeelde in-flight task per key (`ConcurrentDictionary<key, Lazy<Task>>`), een lock per key, of `HybridCache`, die stampede-bescherming ingebouwd heeft.
</details>
