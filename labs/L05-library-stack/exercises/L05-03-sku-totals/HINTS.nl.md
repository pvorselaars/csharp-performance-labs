# Hints (open er één per keer)

<details><summary>Hint 1: welke tool?</summary>

De profiler toont tijd in SQLite's native read-path, niet in jouw code. Gebruik de eigen tool van de database: vraag SQLite om het query-plan (`EXPLAIN QUERY PLAN SELECT …`), of zet EF command-logging aan om de SQL te zien.
</details>

<details><summary>Hint 2: waar?</summary>

Het plan zegt `SCAN Lines` (of `SCAN TABLE`) voor een query die op één kolom filtert. Hoe zou een *search* er in plaats daarvan uitzien?
</details>

<details><summary>Hint 3: waarom?</summary>

Zonder index leest de database alle 200.000 rijen voor elke query: 100 queries × 200k rijen. Een index op de gefilterde kolom laat hem direct naar de matchende rijen springen. In EF declareer je dat in het model (`HasIndex`), en hij wordt met het schema aangemaakt.
</details>
