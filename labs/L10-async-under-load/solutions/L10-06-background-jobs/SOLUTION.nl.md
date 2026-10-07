# L10-06 - Oplossing

## Wat het profiel laat zien
- **Counters:** thread count en queue length pieken tijdens de burst; HTTP-p99 loopt op.

## Grondoorzaak
Onbegrensd fire-and-forget (`Task.Run` per request) verwijdert alle back-pressure: achtergrond-concurrency is gelijk aan request-concurrency, en blokkerende taken concurreren met het afhandelen van requests om de thread pool.

## Fix
Een begrensde `Channel<T>` die N langlopende workers voedt. Het endpoint zet in de wachtrij (en awaiet als die vol is) en geeft 202 terug. Gebruik in productie een `BackgroundService`, bepaal wat een volle wachtrij betekent (429/503 of wachten), en persisteer taken als ze een herstart moeten overleven.

## Lessen
1. **Fire-and-forget is onbegrensd werk onder een andere naam.** Alles wat je niet kunt begrenzen is een uitval die op een burst wacht.
2. Achtergrondwerk deelt de resources van het proces; isoleer het (dedicated workers, begrensde parallellie, eventueel een aparte pool).
3. Bepaal vooraf het faalbeleid voor een volle wachtrij: wachten, weigeren, oudste laten vallen, of doorzetten naar duurzame opslag.
4. Niet-geobserveerde exceptions in `Task.Run` gaan stilletjes verloren tenzij ze afgehandeld worden: nog een reden om een bewaakte worker te gebruiken.

## Extra credit
Maak de taken `async` (await een delay van 5 ms) in plaats van blokkerend. Heb je de begrensde wachtrij dan nog nodig?

## Verder
Geef direct 429 terug als de wachtrij vol is (`TryWrite`) in plaats van te awaiten. Wat ziet de client, en hoe zou die opnieuw proberen?
