# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

Counters per laag: cache-loads per key, calls naar de externe partij per request (`externalCalls`), mislukkingen (`giveUps`), pool-waiters. Teken het request-pad en meet elke hop.
</details>

<details><summary>Hint 2: waar?</summary>

Fix eerst de laag die het dichtst bij de gebruiker ligt, meet daarna opnieuw; het volgende probleem ziet er anders uit. Vraag je van elke laag af: is hij begrensd? is hij gedeeld? houdt hij een schaarse resource vast tijdens het wachten?
</details>

<details><summary>Hint 3: waarom?</summary>

Dit is L12-02 (stampede), L12-05 (retry storm) en L11-03 (connectie vastgehouden tijdens een trage call) in één request-pad. Elk versterkt de volgende: een koude cache veroorzaakt gelijktijdige loads → de externe partij raakt overbelast → directe retries voegen nog meer load toe → connecties worden langer vastgehouden → de pool raakt uitgeput.
</details>
