# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

dotTrace's **timeline** laat zien dat threads grotendeels *wachten op een monitor*; `monitor-lock-contention-count` in `dotnet-counters` loopt snel op. Sampling laat de tijd in `Audit` zien, maar steeds maar één thread tegelijk.
</details>

<details><summary>Hint 2: waar?</summary>

Alle acht threads staan in de rij bij één `lock`. Kijk naar *wat er wordt uitgevoerd terwijl die wordt vastgehouden*: welke van die statements heeft de lock eigenlijk nodig?
</details>

<details><summary>Hint 3: waarom?</summary>

Een lock serialiseert alles wat erbinnen zit. Werk dat alleen van zijn eigen argument afhangt, heeft geen bescherming nodig; alleen de gedeelde schrijfactie wel. Kan die gedeelde schrijfactie veilig zonder lock (één atomaire operatie)?
</details>
