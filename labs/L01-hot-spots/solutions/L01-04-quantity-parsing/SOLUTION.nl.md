# L01-04 - Oplossing

## Wat het profiel laat zien
Onder `decimal.Parse`: de exception-machinerie: throwen, de stack trace vastleggen, unwinden naar de catch,
plus allocatie van de exception-objecten (27 MB toegewezen vóór de fix vs. 4,6 MB erna).
Exacte framenamen verschillen tussen .NET-versies.

## Grondoorzaak
Zo'n helft van de invoer is geen getal, dus bij zo'n helft van de iteraties wordt een `FormatException`
**gegooid en gevangen**. Exceptions zijn voor het uitzonderlijke; een routinematige datacondities zou daar
niet als control flow voor moeten dienen. De kosten van een throw groeien met de stackdiepte, dus in een
echte (diepere) call stack zou dit erger zijn dan hier.

## Fix
`decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out var value)`. Dezelfde
acceptatieregels als `decimal.Parse` met die argumenten, dus de checksum klopt nog.

## Take-aways
1. De BCL heeft om een reden een `Try...`-variant. Gebruik die wanneer falen een verwachte uitkomst is.
2. Nieuwere runtimes maakten exception handling aanzienlijk goedkoper (.NET 9 herschreef de unwinder in
   managed code), en juist daarom is deze workload groot opgezet: de exercise moet falen op *elke* runtime, niet
   alleen oude. Snellere exceptions zijn nog steeds veel trager dan een teruggegeven `false`.
3. **Een debugger maakt dit dramatisch erger** (first-chance exception-notificaties). Probeer de trage versie
   onder Debug vs Profile om het te voelen. Neem nooit timings op met een gekoppelde debugger.
4. Goedkope productiecheck: `dotnet-counters monitor System.Runtime` toont een `exception-count`-rate. Een
   constant niet-nul rate op een gezonde service is een geur die onderzoek verdient.

## Verder
Voeg een derde invoerklasse toe ("1.234,50" met duizendtalscheidingstekens, of `null`) en zorg dat jouw fix
`null` afhandelt zonder te gooien (`TryParse` accepteert null en geeft false terug).
