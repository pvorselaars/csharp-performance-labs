# Hints (open ze één voor één)

<details><summary>Hint 1: welke tool?</summary>

Behouden-na-GC in de harness; dotMemory: één grote `MemoryCache`-entries-dictionary.
</details>

<details><summary>Hint 2: waar?</summary>

Wat begrenst het aantal entries in de cache, en wat verwijdert een entry?
</details>

<details><summary>Hint 3: waarom?</summary>

`MemoryCache` heeft **geen size limit en geen standaard-expiratie**: entries blijven staan tot jij anders zegt. Zet `SizeLimit` op de cache *en* een size per entry (`SetSize`), plus een expiratie. Als de limiet bereikt is, worden nieuwe entries niet toegevoegd (of oudere compacteren weg) in plaats van te blijven groeien.
</details>
