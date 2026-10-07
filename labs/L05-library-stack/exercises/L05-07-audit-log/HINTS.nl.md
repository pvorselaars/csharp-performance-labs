# Hints (open er één per keer)

<details><summary>Hint 1: welke tool?</summary>

dotTrace-sampling, kijkend naar *system*-tijd: frames in `File.OpenHandle`, `SafeFileHandle`, `RandomAccess.WriteAtOffset`, `close`. Op Linux telt `strace -c -f` de syscalls.
</details>

<details><summary>Hint 2: waar?</summary>

Welke .NET-call wordt 20.000 keer aangeroepen, en wat doet *elke* call met het bestand?
</details>

<details><summary>Hint 3: waarom?</summary>

`File.AppendAllText` opent het bestand, schrijft, en sluit het, elke keer opnieuw: meerdere systeemaanroepen (open/write/close, en metadata) per 20 bytes. Een `StreamWriter` met een buffer bundelt veel kleine writes tot een paar grote. Wat is de afweging als het proces crasht voordat de buffer geflusht is?
</details>
