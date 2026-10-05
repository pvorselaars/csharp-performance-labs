# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

Gebruik dotTrace's **timeline** of een GC-view, en vergelijk gen1-aantallen. Kijk ook naar *welke threads* er draaien. Is er een thread die jij niet hebt aangemaakt die werk aan het doen is?
</details>

<details><summary>Hint 2: waar?</summary>

De tijd zit niet in `Paint` of `Fingerprint`. Hij zit in allocatie en in de GC zelf. Kijk naar de members van `Tile` die *geen* velden of methodes zijn die je zelf aanroept.
</details>

<details><summary>Hint 3: waarom?</summary>

Een klasse met een finalizer wordt bij allocatie geregistreerd bij de runtime (een trager allocatiepad), en wanneer hij sterft kan de GC hem niet zomaar vrijgeven: hij komt op de finalisatiequeue en **overleeft** minstens nog één collectie, waardoor hij gepromoveerd wordt. Bezit `Tile` iets dat een finalizer nodig heeft?
</details>
