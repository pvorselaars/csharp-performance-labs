# Hints (open er één per keer)

<details><summary>Hint 1: welke tool?</summary>

Sampling-profiel, Release. Sorteer op **eigen tijd** (own time). De allocatie is piepklein, dus dotMemory heeft niets te melden: dit is een CPU-probleem.
</details>

<details><summary>Hint 2: waar?</summary>

Het frame met de meeste eigen tijd zit niet in jouw code. Loop *omhoog* door de call tree naar het eerste frame dat dat wel is: welke regel van `Feed.Build` is dat, en welke BCL-methode roept die aan?
</details>

<details><summary>Hint 3: waarom?</summary>

Onder de motorkap is `List<T>` een array. Invoegen op index 0 moet elk bestaand element één plek naar rechts opschuiven. Hoeveel elementen verschuiven bij de 1e insert? Bij de 60.000e? Tel ze op.
</details>
