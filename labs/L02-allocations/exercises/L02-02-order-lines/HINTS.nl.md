# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

dotMemory **allocations**, gegroepeerd op *allocation site* (call stack), niet op type. Vergelijk dat daarna met de type-view. Er komen meerdere types naar voren; dat is een aanwijzing dat het niet één bug is.
</details>

<details><summary>Hint 2: waar?</summary>

Alles gebeurt binnen `LineParser.Parse`. Tel de allocerende calls per regel: `Split`, `Trim`, `ToUpperInvariant`, `ToLowerInvariant`, `Select`, `Where`, `Aggregate`. Welke daarvan produceren een nieuwe `string` of `string[]`? Welke produceren iterator- of delegate-objecten?
</details>

<details><summary>Hint 3: waarom?</summary>

Elke `Split` alloceert een array *en* een string per stuk. `Trim`/`ToLower` alloceren opnieuw wanneer ze de tekst wijzigen. De lambda's vangen `disabledFlags`, dus de compiler alloceert een closure-object en delegates per call. Vraag jezelf af: kan ik *naar* een deel van de regel kijken zonder het te kopiëren? (`ReadOnlySpan<char>`). En moet de `HashSet<string>` van disabled flags überhaupt een set van strings zijn?
</details>
