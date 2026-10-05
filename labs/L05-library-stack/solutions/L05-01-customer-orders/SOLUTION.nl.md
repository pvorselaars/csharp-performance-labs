# L05-01 - Oplossing

## Wat het profiel laat zien

- **SQL-view / EF-logging:** ~501 commando's: `SELECT … FROM Orders WHERE CustomerId = @p` × 500 plus één klanten-query.
- **Sampling:** tijd binnen EF's query-pipeline (compile/cache-lookup, command-creatie, materialisatie), 500x herhaald.

## Grondoorzaak
De klassieke **N+1**: één query om N ouders op te halen, dan N queries (één per ouder) om hun kinderen op te halen. Kosten zijn N x overhead-per-query, wat lokaal piepklein is maar enorm tegen een database over een netwerk.

## Fix
Vraag de database één keer: projecteer naar `{ Id, Total = c.Orders.Sum(...) }` zodat het aggregaat server-side in één query berekend wordt. Alternatieven: `Include(c => c.Orders)` (laadt alle order-rijen), of `GroupBy` op `Orders`. Gebruik `AsNoTracking()` omdat er niets geüpdatet wordt.

## Lessen
1. **Tel de statements, niet alleen de tijd.** N+1 verstopt zich omdat elke query op zich snel is.
2. Loops die de database aanroepen zijn verdacht; vraag je af of één set-gebaseerde query ze kan vervangen.
3. `Include` voorkomt N+1 maar laadt hele rijen; een projectie laadt alleen wat je nodig hebt (vaak het beste).
4. Preventie: log of tel commando's per request in tests (de `sqlCommands`-metric van deze harness) en laat falen als die een budget overschrijdt.

## Extra credit
Verhoog naar 5.000 klanten in een scratch-kopie. Hoe schalen de twee versies?

## Ga verder
Implementeer het met `Include` en met `GroupBy`; vergelijk `sqlCommands`, allocaties en tijd. Welke laadt de meeste data?
