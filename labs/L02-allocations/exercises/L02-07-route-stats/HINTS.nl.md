# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

dotMemory allocations op type: wat wordt er één keer per event gealloceerd? Dan dotTrace: wat roept de lus per event aan?
</details>

<details><summary>Hint 2: waar?</summary>

De key wordt per event opgebouwd met string-interpolatie, en vervolgens gebruikt voor `GetValueOrDefault` en de indexer: twee hash-lookups, die beide de hele string hashen, en de *key-strings voor herhaalde combinaties worden allemaal weggegooid* behalve de eerste.
</details>

<details><summary>Hint 3: waarom?</summary>

Een samengestelde string-key betekent het alloceren en hashen van een verse string om iets te identificeren dat eigenlijk drie getallen is. Welk type draagt meerdere waarden, heeft value equality en een goede hashcode, en leeft inline zonder heap-object? (Controleer dan: implementeert het type dat je kiest `IEquatable<T>`?)
</details>
