# Hints (open er één per keer)

<details><summary>Hint 1: welke tool?</summary>

dotMemory-allocaties: `string` en `DefaultInterpolatedStringHandler`/`char[]` vanaf de log-call-site, ook al bestaat er geen log-uitvoer.
</details>

<details><summary>Hint 2: waar?</summary>

Het argument van `LogDebug` is een *interpolated string*. Wanneer wordt die geëvalueerd: voor de call, of alleen als het niveau enabled is?
</details>

<details><summary>Hint 3: waarom?</summary>

C# evalueert en formatteert argumenten **voordat** de methode draait, dus `LogDebug($"…{x}…")` bouwt de string zelfs als Debug uitstaat. Een *message template* met argumenten stelt formattering uit; `LoggerMessage` (of de `[LoggerMessage]`-source-generator) voorkomt ook boxing en checkt eerst `IsEnabled`. Wat geeft nul allocatie wanneer uitgeschakeld?
</details>
