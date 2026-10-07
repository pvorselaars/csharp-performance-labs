# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

Kloktijd verbergt dit, omdat de duplicaten parallel draaien. Kijk naar *CPU-tijd* (de harness rapporteert die) en naar het **aantal calls** van `LoadSection` (tracing-modus, of de metric `factoryCalls`).
</details>

<details><summary>Hint 2: waar?</summary>

`ConcurrentDictionary.GetOrAdd(key, factory)` is thread-safe voor de *dictionary*, maar lees wat de documentatie zegt over de *factory*.
</details>

<details><summary>Hint 3: waarom?</summary>

`GetOrAdd` kan de factory op meerdere threads tegelijk aanroepen en houdt dan maar één resultaat over. Het werk van de rest wordt weggegooid. Wat kun je in de dictionary opslaan dat het dure werk uitstelt, en garandeert dat het maar één keer draait?
</details>
