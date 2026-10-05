# L12-boss - Oplossing

## Grondoorzaak
Eén defect uit drie Lab 12-exercises.

## Fix
`IHttpClientFactory`; single-flight per key (een gedeelde `Lazy<Task>`-map voor in-flight loads); een begrensde `MemoryCache` (`SizeLimit`, `SetSize`).

## Lessen
1. **Defect -> bron:** client per request = **L12-01**; niet-atomaire get-or-create = **L12-02**; onbegrensde `MemoryCache` = **L12-03**.
2. Elk heeft zijn eigen metric (connections, downstream calls, behouden geheugen): drie verschillende tools, één endpoint.
3. De lange staart van eenmalige id's maakt een onbegrensde cache gevaarlijk, terwijl het herhaald verlopen van de populaire id's ervoor zorgt dat de stampede zich herhaalt in plaats van een eenmalige opstartkost te zijn.

## Extra credit
Wat doet een `SizeLimit` van 15 (onder de populaire set van 20) met het aantal verbindingen?

## Ga verder
Vervang de handgebouwde single-flight door `HybridCache` en vergelijk.
