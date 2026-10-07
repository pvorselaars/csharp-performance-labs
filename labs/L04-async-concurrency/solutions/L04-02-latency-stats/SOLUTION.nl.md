# L04-02 - Oplossing

## Wat het profiel laat zien

- **Timeline:** op elk moment zijn 7 van de 8 threads geblokkeerd op de monitor; het contention-aantal loopt in de honderdduizenden.
- **Sampling:** `Audit` domineert de self time, en de totale CPU zit op ≈ 1 core.

## Grondoorzaak
De critical section van de lock bevat de dure `Audit`-call, die alleen zijn eigen argument nodig heeft. Alle threads nemen om de beurt dat werk voor hun rekening, waardoor parallelle workers zich gedragen als één thread plus lock-overhead (en extra context switches zodra de lock contended is).

## Fix
Verklein de critical section tot het kleinste wat atomair moet zijn: bereken `Audit` erbuiten, en update dan de gedeelde bucket met `Interlocked.Add` (geen lock). Andere opties: per-thread buckets die aan het eind samengevoegd worden (helemaal geen delen) of striped locks.

## Lessen
1. **Houd locks zo kort mogelijk vast**, en nooit over werk heen dat geen gedeelde state aanraakt (of over I/O of `await` heen).
2. Contention uit zich als *wachten*, niet als CPU: lage CPU met veel threads betekent kijken naar de timeline en de lock-tellers.
3. `Interlocked` en per-thread accumulatie verslaan locks voor tellers; meet het, want false sharing (Lab 6) kan de striped versie parten spelen.
4. Correctheid eerst: de checksum controleert of de atomaire versie nog steeds klopt.

## Extra credit
Houd de lock, maar verplaats `Audit` erbuiten. Hoeveel van de winst krijg je daarmee? Wat blijft er over, en waarom?

## Go further
Implementeer per-thread buckets (`ThreadLocal<long[]>` of `Parallel.For` met lokale state) die aan het eind samengevoegd worden. Vergelijk dat met `Interlocked` bij 8 en bij 32 workers.
