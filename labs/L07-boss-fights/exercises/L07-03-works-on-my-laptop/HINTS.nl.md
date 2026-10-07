# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

Vergelijk de eerste regels van de harness (GC-modus, cores) en `committedMB` bij verschillende core-aantallen (`taskset -c 0-1`, `0-3`, alle), en lees `bin/.../*.runtimeconfig.json` in de outputmap. `dotnet-counters`: `gc-committed`, `gen-0-gc-count`, en `gc-heap-size`.
</details>

<details><summary>Hint 2: waar?</summary>

Er is geen C# om te veranderen. Welke *configuratie* bepaalt hoeveel heaps de GC aanmaakt en hoe groot het allocatiebudget van elke heap is?
</details>

<details><summary>Hint 3: waarom?</summary>

Server-GC maakt per core één heap (en één GC-thread) aan met een groot gen0-budget per heap: uitstekende doorvoer op een grote machine, maar een groot geheugenbeslag zelfs voor een kleine workload, vooral met dynamische aanpassing (DATAS) uitgeschakeld. Opties: minder heaps (`GCHeapCount`), dynamische aanpassing, of Workstation-GC.
</details>
