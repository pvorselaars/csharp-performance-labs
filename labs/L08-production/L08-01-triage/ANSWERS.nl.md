# Antwoorden

| Scenario | Diagnose | Wat de counters lieten zien |
|---|---|---|
| **a** | **CPU-bound** | `dotnet.process.cpu.time` met `cpu.mode=user` ≈ 4 per 1 s-interval ≈ 4 cores bezig (van `cpu.count` = 20, dus ~20% van de machine);<br/>`cpu.time[mode=system]` ≈ 0. GC inactief (0 collecties in elke generatie, geen pauzes);<br/> thread-pool-counters allemaal 0 (de bezette threads moeten dedicated `Thread`s zijn, geen pool-threads); |
| **b** | **GC-druk** | ~**200–250 gen2-collecties per seconde**<br/>~0 gen0/gen1: elke collectie is een gen2 omdat de LOH hem triggert);<br/>GC-pauze = 0,08–0,09 s per seconde (~8-9% van wall time);<br/> allocatie ≈ 2-3 GB per s;<br/> CPU slechts ≈ 0,4–0,5 core |
| **c** | **Thread-pool starvation** | CPU ≈ **0,1 core** (bijna idle);<br/>pool **queue length ≈ 70-180** die niet leegloopt;<br/>pool **thread count ≈ 80 -> 200 en oplopend** (de meter toont dit als ongeveer +10 threads per 2 s);<br/>~10–30 lock contentions per 2 s uit interne `Task.Wait`-aanroepen, **dezelfde range als d**. Na ~25 s heeft de pool genoeg threads bijgespijkerd (~200) dat de starvation opheft: de queue loopt leeg, CPU en doorvoer schieten omhoog. **Meet binnen de eerste 20 s.** |
| **d** | **Lock contention** | CPU ≈ 0,05 core;<br/>**~20 lock contentions per 2 s**, stabiel; *geen* pool-activiteit, helemaal niets: 0 threads, 0 queue, 0 werkitems (gebruikt dedicated threads) |
| **e** | **Gezond** | CPU ≈ 0,05 core;<br/>~14 pool-threads, **queue 0**, ~2000 werkitems/s die soepel afronden;<br/>geen GC;<br/>af en toe een losse contention. Niets verzadigd: niet fixen |

## Vraag 1: welke twee lijken op elkaar, en wat scheidt ze?
**c en d** (allebei idle CPU, traag werk, en *beide* laten lock contentions zien in een vergelijkbare rate, dus `lock_contentions` maakt **geen** onderscheid). De scheidende counter is de **thread pool**: in **c** is de queue length groot en blijft het thread-aantal oplopen. In **d** staat elke pool-counter vlak op 0, omdat de contending threads dedicated `Thread`s zijn. Dus c is "pool-counters + contentions" en d is "alleen contentions".

## Vraag 2: welke zou een CPU sampling profile niet kunnen verklaren?
**c en d** (en **e**, wat prima is): threads die *wachten* staan niet op de CPU, dus een CPU-profile is bijna leeg. Gebruik de **timeline/trace**-view of thread-stacks (`dotnet-stack`/`dotnet-dump analyze`, `clrstack`) om te zien waarop ze wachten.

## Vraag 3: welke zou je niet fixen?
**e**. Lage bezetting en geen verzadiging op enige resource is geen probleem; erachteraan jagen verspilt tijd (en de USE-methode zegt: geen resource is verzadigd, er zijn geen fouten).

## Vraag 4: volgende tool
| Scenario | Volgende tool | Wat je hoopt te zien |
|---|---|---|
| a | `dotnet-trace` (sampling) → flame graph | de hot method (dit is Lab L08-02) |
| b | `dotnet-counters` LOH-grootte + `dotnet-gcdump`/allocatie-trace | wie de grote arrays alloceert; dan pooling/hergebruik (Lab 2) |
| c | `dotnet-stack`/dump `clrstack` op pool-threads | veel threads in `Task.Wait`/`GetResult`: vind en verwijder de sync-over-async (Lab 4) |
| d | dump `syncblk`/`threads` | één thread houdt de monitor vast terwijl anderen wachten; verklein de kritieke sectie (Lab 4) |

## De gewoonte die dit oefent
Classificeer eerst op **resource** (USE: utilisation, saturation, errors), kies daarna de zwaardere tool.

## Onthulling
- `a` draait busy loops op 4 threads
- `b` alloceert constant arrays van 100–300 KB
- `c` bursts van 200 `Task.Run(() => asyncCall().Result)` met de pool vastgepind op 4 threads
- `d` 16 threads die één lock nemen en er 2 ms binnen slapen
- `e` 200 async loops die wachten op `Task.Delay`.
