# L05-06 - Oplossing

## Wat het profiel laat zien

- **Allocaties:** een `string` per call (het geïnterpoleerde bericht), plus het boxen van `long`/format-werk; er wordt nergens iets weggeschreven.

## Grondoorzaak
Geïnterpoleerde strings die aan `ILogger` worden doorgegeven, worden gretig geformatteerd, op de call-site, ongeacht het geconfigureerde niveau. Uitgeschakelde logging betaalt nog steeds voor het bouwen van het bericht.

## Fix
Gebruik source-generated `[LoggerMessage]`-methodes (of `LoggerMessage.Define`): sterk getypeerde argumenten, geen boxing, en formattering alleen als het niveau enabled is. Message templates (`"…{OrderId}…", i`) zijn op één na het beste: ze stellen formattering uit maar boxen nog steeds value-type-argumenten.

## Lessen
1. **Gebruik nooit string-interpolatie voor logberichten.** Dat ondermijnt niveau-filtering en structured logging (analyzer CA2254 waarschuwt ervoor).
2. Uitgeschakeld != gratis: de *argumenten* worden nog steeds geëvalueerd. Bescherm dure argumenten met `IsEnabled`.
3. `[LoggerMessage]` is het nul-allocatie-, structured-, snelle pad; gebruik het in hot code.
4. Structured templates houden de properties (`OrderId`, `Customer`) doorzoekbaar in je logopslag.

## Extra credit
Voeg een `if (Log.IsEnabled(LogLevel.Debug))`-guard toe aan de *geïnterpoleerde* versie. Verwijdert dat de kosten? Wat is het nadeel ten opzichte van `[LoggerMessage]`?

## Ga verder
Vergelijk drie versies: geïnterpoleerd, template met argumenten, source-generated. Zet daarna Debug aan en vergelijk opnieuw. Welke is het beste wanneer enabled?
