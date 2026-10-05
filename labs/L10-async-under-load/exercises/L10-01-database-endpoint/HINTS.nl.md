# Hints (open ze één voor één)

<details><summary>Hint 1: welk tool?</summary>

`dotnet-counters`: thread-pool queue length en thread count terwijl de belasting draait (zie `labs/L08-production/README.md` voor de nieuwe counter-namen). dotTrace-timeline: pool-threads *Waiting*.
</details>

<details><summary>Hint 2: waar?</summary>

Welke call in de handler blokkeert de thread terwijl de 'database' werkt?
</details>

<details><summary>Hint 3: waarom?</summary>

Dit is L04-01 binnen een echte pipeline. Een pool-thread blokkeren per request die onderweg is, betekent dat 200 gelijktijdige requests 200 threads nodig hebben, maar de pool groeit langzaam; requests wachten in de rij op threads terwijl de threads wachten op de continuation van de delay. Maak de handler end-to-end async en laat het framework erop awaiten.
</details>
