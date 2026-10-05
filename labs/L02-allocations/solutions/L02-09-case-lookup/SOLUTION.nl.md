# L02-09 - Oplossing

## Wat het profiel laat zien
- **Allocaties:** een `string` per opzoeking (≈ 60 bytes) van `ToLowerInvariant`, tenzij de input al lowercase is.

## Grondoorzaak
Bij elke opzoeking een genormaliseerde kopie van de key alloceren in plaats van een hoofdletterongevoelige comparer te gebruiken.

## Fix
Construeer de dictionary met `StringComparer.OrdinalIgnoreCase` en zoek op met de ruwe key. (Gebruik `Ordinal*`-comparers voor identifiers en protocoltekst; cultuurbewuste comparers zijn trager en zelden wat je wilt.)

## Lessen
1. **Kopieer niet om te vergelijken.** Comparers bestaan juist zodat de data niet van vorm hoeft te veranderen.
2. `Ordinal` versus cultuurgevoelig: kies bewust (en let op dat `InvariantGlobalization` in deze repo aanstaat).
3. `ToLower`/`ToUpper` op gebruikersinvoer heeft ook correctheidsvalkuilen (het Turkse-i-probleem): nog een reden om comparers te gebruiken.

## Extra credit
Wat doet de dictionary voor hashing wanneer de comparer `OrdinalIgnoreCase` is? Kijk hoe die allocatie vermijdt.

## Ga verder
Gebruik `Dictionary<string,T>.GetAlternateLookup<ReadOnlySpan<char>>()` (.NET 9+) om spans op te zoeken zonder ook maar één string aan te maken.