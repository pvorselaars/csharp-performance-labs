# L05-04 - Oplossing

## Wat het profiel laat zien
- **Metric:** `connections` = 300
- **Sampling:** constructie van `HttpClient`/`SocketsHttpHandler` en `Socket.Connect` domineren over het request-werk.

## Grondoorzaak
Een nieuwe `HttpClient` (en dus een nieuwe `SocketsHttpHandler` en connection pool) per call. Elk request opent een nieuwe TCP-verbinding (en TLS-sessie, tegen HTTPS), en gesloten sockets blijven hangen in `TIME_WAIT`. Onder load wordt dit **socket exhaustion**.

## Fix
Hergebruik: een `static`/singleton `HttpClient`, of (in DI-apps) `IHttpClientFactory` / typed clients. Als de client langlevend is, zet `PooledConnectionLifetime` (bijv. 2 minuten) zodat DNS-wijzigingen worden opgepikt.

## Lessen
1. **`HttpClient` is bedoeld om hergebruikt te worden**, niet per request aangemaakt; het disposen ervan geeft je de poort niet direct terug.
2. Het symptoom op schaal: `SocketException: Address already in use` of een stapel `TIME_WAIT`, vaak pas onder load.
3. Langlevende clients hebben `PooledConnectionLifetime` nodig zodat DNS-wijzigingen gerespecteerd worden; `IHttpClientFactory` regelt dat voor je (zie Lab 12).
4. Tel *verbindingen*, niet alleen requests, wanneer je een uitgaande client test.

## Extra credit
Draai `ss -tan | grep -c TIME-WAIT` (Linux) voor en na elke versie.

## Ga verder
Gebruik `IHttpClientFactory` vanuit een kleine `ServiceCollection`, en vergelijk `connections`. Zet daarna `PooledConnectionLifetime` heel klein: wat gebeurt er met de telling?
