# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

dotMemory allocations op type: welk type, en hoeveel instanties (ongeveer gelijk aan het aantal calls)?
</details>

<details><summary>Hint 2: waar?</summary>

Welke regel maakt een object per call? Kijk hoe `foreach` over een parameter met een *interface-type* wordt gecompileerd.
</details>

<details><summary>Hint 3: waarom?</summary>

`List<T>` heeft een struct-enumerator die `foreach` zonder allocatie gebruikt wanneer het statische type `List<T>` is. Doorgegeven als `IEnumerable<T>` wordt dezelfde struct per call **geboxt** (heap-allocatie), en elk element gaat via interface-dispatch. Accepteer een concreet type of, beter nog, een span.
</details>
