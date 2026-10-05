# L10-05 - Oplossing

## Wat het profiel laat zien


## Grondoorzaak
De handler observeert `RequestAborted` nooit: elke afgebroken request loopt gewoon tot voltooiing. Hier is de gedeelde resource een `SemaphoreSlim` met 4 slots. Afgebroken requests wachten op een slot (en blijven in de rij staan nadat de client al weg is), en houden het dan de volle 200 ms vast. In echte code is het slot een databaseverbinding, een thread, of de capaciteit van een downstream-call.

Let op wat *niet* verandert: de eigen timing van de client (elke ongeduldige client geeft het hoe dan ook na 30 ms op). De winst zit in de service, en in de clients die wél gebleven zijn.

## Fix
Geef `ctx.RequestAborted` door aan elke geawaite operatie (hier `Capacity.WaitAsync(ct)` en `Task.Delay(10, ct)`; in echte code `SaveChangesAsync(ct)`, `SendAsync(req, ct)`, `ReadAsync(ct)`). Een `OperationCanceledException` door een clientdisconnect is normaal en wordt niet als fout gelogd. De `finally`-blokken geven het slot vrij en verlagen de inflight-teller zodra de exception afwikkelt.

## Lessen
1. **Cancellation is coöperatief: je moet de token zelf doorgeven.** Eén call zonder token breekt de hele keten.
2. Verspild werk onder retries is een positieve feedback loop; cancellation is onderdeel van overload-bescherming.
3. Voeg ook *binnen* de server timeouts toe (een gekoppelde `CancellationTokenSource` met een deadline), zodat trage downstreams requests niet eeuwig vasthouden.
4. Accepteer `CancellationToken ct` in je eigen async methodes en geef hem door; analyzers (CA2016) signaleren ontbrekende doorgave.

## Extra credit
Maak het trage werk CPU-bound (een lus) in plaats van awaits. Hoe maak je *dat* annuleerbaar?

## Verder
Voeg een server-side deadline van 100 ms toe door `RequestAborted` te koppelen aan een `CancellationTokenSource(100)`. Welke status zou de client moeten zien?
