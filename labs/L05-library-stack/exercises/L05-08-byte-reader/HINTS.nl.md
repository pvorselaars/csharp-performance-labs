# Hints (open er één per keer)

<details><summary>Hint 1: welke tool?</summary>

dotTrace met system-frames / `strace -c`: hoeveel `read`-systeemaanroepen doet de run?
</details>

<details><summary>Hint 2: waar?</summary>

Wat is `bufferSize: 0`, en wat betekent dat voor elke `ReadByte()`?
</details>

<details><summary>Hint 3: waarom?</summary>

Een `FileStream` leest normaal gesproken een blok (standaard 4 KB) en bedient bytes vanuit zijn buffer. Met buffering uitgeschakeld is elke `ReadByte` een OS-call. Zet buffering aan (of lees het hele bestand / blokken in een array).
</details>
