# L05-02 - Oplossing

## Wat het profiel laat zien
- **allocaties:** `Product` (10.000x3) en hun `string`-Descriptions domineren, plus EF's change-tracker-entries (`InternalEntityEntry`, snapshots).
- **SQL-view:** drie `SELECT` van alle kolommen zonder `WHERE`.

## Grondoorzaak
Het filter en de kolomselectie gebeuren in C# nadat alles geladen is, en elke load gaat door de change tracker. Te veel rijen ophalen, te veel kolommen ophalen, en tracking-overhead betalen voor read-only data.

## Fix
`AsNoTracking()` + `Where` (vertaald naar SQL) + `Select` naar een kleine projectie. Nu filtert de database, reizen er maar twee kolommen mee, en trackt EF niets.

## Lessen
1. **Duw filters naar de database en selecteer alleen de kolommen die je nodig hebt.**
2. Tracking is voor entities die je gaat wijzigen; read-only queries moeten `AsNoTracking` gebruiken (of projecties, die sowieso niet getrackt worden).
3. 'Alles laden en dan `.Where` in-memory' ziet er identiek uit aan een server-side filter wanneer een methode `IEnumerable` teruggeeft: let op premature `ToList()`.
4. Brede kolommen (tekst/blobs) maken over-fetching veel erger: overweeg table splitting.

## Ga verder
Vergelijk `AsNoTracking()` op de *volledige-entity*-query versus de projectie, om de trackingkosten los te zien van de over-fetch-kosten.
