# L11-02 - Oplossing

## Wat het profiel laat zien
- **SQL:** één query met twee joins die 400 rijen teruggeeft; na de fix drie queries die 1 + 20 + 20 = 41 rijen teruggeven. **Metric:** `rowsPerRequest` 400 -> 41.

## Grondoorzaak
Twee collectie-navigaties in één JOIN vermenigvuldigen rijen (cartesiaans product), wat datatransport, materialisatie en geheugen opblaast.

## Fix
`AsSplitQuery()` (extra round trips maar veel minder data), of projecteer naar een DTO met aggregaten. Split queries zijn niet gratis: weeg consistentie en round trips af tegen een database over het netwerk.

## Lessen
1. **Meerdere collectie-Includes vermenigvuldigen rijen.** EF waarschuwt er niet voor niets voor.
2. Vergelijk teruggegeven rijen, niet alleen het aantal queries: de gematerialiseerde `Customer.Orders`/`Addresses`-collecties zijn hoe dan ook dezelfde 20+20, omdat EF de herhaalde parent-kolommen dedupliceert bij het bouwen van de objectgraaf. Alleen een rijentelling onder EF (bijvoorbeeld door de `DbDataReader` te omwikkelen, zoals `RowCounter` hier doet) legt de cartesiaanse explosie bloot.
3. Projectie (`Select`) wint meestal van `Include` voor leesmodellen.

## Extra credit
Vergroot beide collecties naar 100 items. Hoe schalen de twee versies?

## Verder gaan
Projecteer naar `{ Total = c.Orders.Sum(...), Addresses = c.Addresses.Count() }` en vergelijk met split queries.
