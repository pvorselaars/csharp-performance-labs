# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

dotMemory allocations op type. Tel hoeveel objecttypes er per *regel* worden aangemaakt. De regex-engine is niet langer het probleem; wat geeft hij terug?
</details>

<details><summary>Hint 2: waar?</summary>

`Regex.Match` alloceert een `Match`, een `GroupCollection`, `Group`-objecten en capture-arrays; `Groups["name"]` zoekt op naam op; `.Value` alloceert een string per groep die je uitleest. Je leest maar drie groepen, maar de engine registreert ze allemaal.
</details>

<details><summary>Hint 3: waarom?</summary>

De regex-API is rond objecten gebouwd, dus die kan niet zero-allocation zijn zodra je groepen moet uitlezen. Het regelformaat is eenvoudig en vast. Zou een handgeschreven parser over `ReadOnlySpan<char>` (`IndexOf`, slicing, `SequenceEqual`) dezelfde velden kunnen vinden zonder ook maar één van die objecten aan te maken? Let goed op dat je *dezelfde* grammatica aanhoudt: de checksum vertelt het je als dat niet zo is.
</details>
