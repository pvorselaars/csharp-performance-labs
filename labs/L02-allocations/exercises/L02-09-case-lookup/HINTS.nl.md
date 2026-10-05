# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

dotMemory allocations op call site: `string` van `ToLowerInvariant`.
</details>

<details><summary>Hint 2: waar?</summary>

Waar wordt er voor elke opzoeking een nieuwe string aangemaakt, en is die string nog nodig na de opzoeking?
</details>

<details><summary>Hint 3: waarom?</summary>

Normaliseren door te kopiëren is één manier om hoofdletterongevoelig te matchen; de andere is de *comparer* leren hoofdletters te negeren. `StringComparer.OrdinalIgnoreCase` (en de dictionary-constructor die hem accepteert) doet zowel hashing als equality zonder te alloceren.
</details>
