# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

dotMemory-allocaties per type, en de **gen2-telling** in de harness-output: `string`, `char[]` en `StringBuilder`-chunks van een omvang die wijst richting de Large Object Heap.
</details>

<details><summary>Hint 2: waar?</summary>

Volg de bytes: request-stream → ? → `Batch`. Tel hoeveel volledige kopieën van de payload er tegelijk bestaan en in welke encoding.
</details>

<details><summary>Hint 3: waarom?</summary>

`ReadToEndAsync` bouwt een **UTF-16-string** (2 bytes per char: ~340 KB hier, ver boven de LOH-drempel van 85.000 bytes) op via groeiende tussenliggende buffers, en `Deserialize(string)` transcodeert hem vervolgens terug naar UTF-8. Dat is L02-03 (LOH-churn) op het request-pad. De properties die het model niet declareert, worden overgeslagen zonder allocatie, dus de string is vrijwel al het afval. `ReadFromJsonAsync` / `DeserializeAsync` parsen UTF-8 rechtstreeks uit de stream.
</details>
