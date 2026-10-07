# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

Eerste vraag: blijft het geheugen *bereikbaar*? Forceer een volledige GC en kijk wat er overblijft (de *kept*-regel van de harness, of een dotMemory-snapshot na *Force GC*). Kijk daarna naar het gen2-aantal.
</details>

<details><summary>Hint 2: waar?</summary>

Het *kept*-cijfer is ~0, dus niets houdt de tijdelijke arrays vast: het geheugencijfer van het proces was garbage dat nog niet verzameld was. Welke regel laat elke batch opdraaien voor een volledige, blokkerende collectie?
</details>

<details><summary>Hint 3: waarom?</summary>

De GC verzamelt wanneer hij geheugen nodig heeft, niet wanneer jouw code klaar is met een object. Garbage die in het geheugen blijft rondhangen is normaal, dus een stijgende lijn op een geheugendashboard is geen bewijs van een lek; behouden grootte na een volledige GC wel. `GC.Collect()` in productiecode maakt dingen bijna altijd trager.
</details>
