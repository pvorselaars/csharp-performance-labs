# Hints (open er één per keer)

<details><summary>Hint 1: welke tool?</summary>

De SQL-view (of EF's command-logging) toont *aantal* en *totale tijd* per statement. Sorteer op aantal. Sampling alleen zou slechts tijd tonen, dun uitgesmeerd over EF-plumbing.
</details>

<details><summary>Hint 2: waar?</summary>

Hetzelfde statement, alleen verschillend in een parameter, wordt ~500 keer uitgevoerd. Zoek de loop in `Run` die dat veroorzaakt.
</details>

<details><summary>Hint 3: waarom?</summary>

Elke iteratie stelt de database één kleine vraag, dus je betaalt de overhead per query 500 keer (parsen, netwerk-round-trip op een echte server). Kan één query de totalen voor *alle* klanten teruggeven? (Een projectie met een aggregaat, of `GroupBy`, of `Include`.)
</details>
