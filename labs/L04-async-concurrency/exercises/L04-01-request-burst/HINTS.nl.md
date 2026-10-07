# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

Sampling laat een idle proces zien, dus dat misleidt je. Gebruik dotTrace's **timeline**: kijk naar thread-states (vooral *Waiting*) en het aantal threads over tijd. Houd ook `threadpool-thread-count` en `threadpool-queue-length` in `dotnet-counters` in de gaten.
</details>

<details><summary>Hint 2: waar?</summary>

De meeste pool-threads zitten te wachten, binnen `Handle`. Zoek de exacte call op die ze laat wachten, en vraag je af waar *die* op wacht.
</details>

<details><summary>Hint 3: waarom?</summary>

`.Result` blokkeert de huidige thread totdat de task klaar is; de continuation van die task heeft een *pool-thread* nodig om te draaien. Als veel requests geblokkeerd zijn, raakt de pool door z'n vrije threads heen en voegt ze maar langzaam toe, dus elke request wacht op een thread die wordt vastgehouden door een andere request die zelf op een thread wacht. Wat zou `Handle` zijn thread kunnen laten opgeven terwijl het wacht?
</details>
