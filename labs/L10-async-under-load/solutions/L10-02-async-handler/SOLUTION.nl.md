# L10-02 - Oplossing

## Wat het profiel laat zien
- **Timeline:** pool-threads in `Thread.Sleep` (of de wait van de synchrone API).
- **Counters:** stijgende queue length bij idle CPU.

## Grondoorzaak
Een synchrone blokkerende call binnen een `async`-methode. De methode is alleen in naam async; de thread wordt de hele 15 ms vastgehouden.

## Fix
Gebruik de asynchrone API (hier `await Task.Delay`; in echte code `ReadAllTextAsync`, `ExecuteReaderAsync`, `SendAsync`).

## Lessen
1. **`async` op de signatuur garandeert niets.** Kijk uit naar synchrone I/O (bestanden, DB-drivers, `Send`, `Stream.Read`), `Thread.Sleep`, `.Wait()`, `.Result`, en `lock` rond traag werk.
2. Analyzers helpen (bijv. `CA1849` roep async methodes aan in async methodes; banned-API-analyzers voor `Thread.Sleep`).
3. Blokkeren op de thread pool degradeert het hele proces, niet alleen één endpoint.
4. Geef de voorkeur aan libraries met echte async I/O; een sync call in `Task.Run` wikkelen verplaatst het blokkeren alleen naar een andere pool-thread.

## Extra credit
Voeg `Kestrel`-gebruik van synchrone IO (`AllowSynchronousIO`) toe aan een scratch-kopie en kijk wat de server je daarover vertelt.

## Verder
Wikkel de synchrone call in `Task.Run` in een scratch-kopie. Helpt dat? Wat kost het?
