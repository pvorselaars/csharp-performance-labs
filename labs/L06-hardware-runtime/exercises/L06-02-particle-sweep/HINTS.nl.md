# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

Sampling wijst opnieuw de optelregel aan als hot. Vraag het de hardware: `perf stat` cache-miss-aantallen, of vergelijk met een benchmark van dezelfde loop over data die wél *sequentieel* is.
</details>

<details><summary>Hint 2: waar?</summary>

Een array van een *class* is een array van **referenties**. Wat moet elke iteratie doen voordat hij `p.X` kan lezen, en waar zou dat object kunnen liggen?
</details>

<details><summary>Hint 3: waarom?</summary>

Elk element is een pointer naar een los heap-object; nadat de allocatievolgorde door elkaar is gegooid (zoals in elk langlopend programma), wijzen opeenvolgende elementen naar ongerelateerde cache lines: een dependent load per element die de prefetcher niet kan voorspellen. Een `struct`-array slaat de velden **inline** op, aaneengesloten.
</details>
