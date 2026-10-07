# Hints (open er één per keer)

<details><summary>Hint 1: welke tool?</summary>

dotMemory-allocaties per type: welke types domineren, en hoe verhouden die zich tot de grootte van het *resultaat*?
</details>

<details><summary>Hint 2: waar?</summary>

Kijk naar het eerste statement binnen de loop: hoeveel rijen en kolommen haalt het op, en waar draait het filter: in SQL, of in C#?
</details>

<details><summary>Hint 3: waarom?</summary>

Drie kosten stapelen zich op: het ophalen van *alle rijen* (filter in-memory), het ophalen van *alle kolommen* (de 1 KB beschrijving), en **change tracking** (EF houdt een snapshot bij van elke entity om updates te kunnen detecteren die je nooit uitvoert). Hoe zou je exact de benodigde rijen en kolommen opvragen, en tracking uitzetten?
</details>
