# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

dotTrace/timeline: threads die wachten op de lock van de logger; `strace -c`: open/write/close per logregel.
</details>

<details><summary>Hint 2: waar?</summary>

Wat doet één `Log`-aanroep, en wat houdt hij vast terwijl hij dat doet?
</details>

<details><summary>Hint 3: waarom?</summary>

De provider opent, voegt toe aan en sluit het bestand onder een globale lock op de request-thread: elk request serialiseert op de disk (L05-07 + L04-02). Een logging-provider zou moeten **enqueuen en teruggeven**, met een achtergrond-writer die batcht naar een buffered stream, en een begrensde queue die bij overbelasting dropt (en telt) in plaats van requests te blokkeren.
</details>
