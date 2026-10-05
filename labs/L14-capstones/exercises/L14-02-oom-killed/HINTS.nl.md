# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

De kept-after-GC- en gen2-regels van de harness; `dotnet-counters`: `gen-2 collections` en LOH-grootte; dotMemory: wat domineert?
</details>

<details><summary>Hint 2: waar?</summary>

Twee dingen alloceren 100 KB per request: wat gebeurt ermee daarna?
</details>

<details><summary>Hint 3: waarom?</summary>

Elk request alloceert een buffer van 100 KB (LOH: L02-03, gen2-stormen) **en** slaat die op in een cache zonder grens (L03-02, L12-03), dus levend geheugen groeit onbeperkt totdat de geheugenlimiet van de container het proces killt. Fix beide: pool scratch-buffers, cache alleen een kleine afgeleide waarde, begrens de cache.
</details>
