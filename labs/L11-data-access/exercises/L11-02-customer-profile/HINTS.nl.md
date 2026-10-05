# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

EF command logging toont één query met twee `LEFT JOIN`s; de `rowsPerRequest`-metric van de harness is het verraderlijke signaal (die telt rijen zoals `DbDataReader.Read()` ze teruggeeft, voordat EF duplicaten terugvouwt tot losse entiteiten). dotMemory: veel gedupliceerde gematerialiseerde waarden.
</details>

<details><summary>Hint 2: waar?</summary>

Hoeveel rijen komen er terug voor één klant: orders + adressen, of orders × adressen?
</details>

<details><summary>Hint 3: waarom?</summary>

Eén query met een `Include` van twee zustercollecties joint ze: rijen = orders × adressen (een **cartesiaanse explosie**), en de kolommen van elke parent herhalen zich op elke rij. `AsSplitQuery()` geeft één query per collectie (2-3 kleine queries), of je projecteert alleen wat je nodig hebt.
</details>
