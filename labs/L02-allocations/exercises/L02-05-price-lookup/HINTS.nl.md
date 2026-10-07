# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

dotMemory allocations, gegroepeerd op type. Welke types worden gealloceerd, en waar komen ze vandaan?
</details>

<details><summary>Hint 2: waar?</summary>

`Task<decimal>` en, op het zeldzame trage pad, async-state-machine-objecten. `GetPriceAsync` is gedeclareerd als `async`; kijk wat er gebeurt op het cache-hit-pad.
</details>

<details><summary>Hint 3: waarom?</summary>

Een `async Task<T>`-methode moet een `Task<T>` retourneren, en tenzij `T` een van de weinige gecachete speciale gevallen is (kleine ints, `true`/`false`) is dat een **nieuw heap-object, zelfs als de methode synchroon voltooit.** Welk returntype laat een synchroon resultaat reizen zonder heap-object?
</details>
