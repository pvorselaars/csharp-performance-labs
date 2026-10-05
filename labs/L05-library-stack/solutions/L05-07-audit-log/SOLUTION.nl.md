# L05-07 - Oplossing

## Wat het profiel laat zien

- **Sampling:** tijd in file open/close en write-syscalls, niet in formattering.
- **`strace -c`:** ~20.000 x `openat`/`write`/`close` (en vrienden) tegenover een handvol grote `write`s.

## Grondoorzaak
Per call open/write/close. Systeemaanroep- en bestandssysteem-overhead domineren kleine writes; de kosten schalen met het *aantal* calls, niet met de bytes.

## Fix
Houd één `StreamWriter` met een grote buffer (64 KB) open voor de hele run en schrijf ernaartoe; flush op een timer of bij afsluiten, afhankelijk van de durability-eisen. Dezelfde bytes op schijf (gebruik `new UTF8Encoding(false)`: `Encoding.UTF8` schrijft een BOM van 3 bytes aan het begin van een nieuw bestand, wat `AppendAllText` niet doet).

## Lessen
1. **Bundel kleine I/O.** Syscalls per call, niet bytes, zijn de kosten.
2. Buffering ruilt durability in voor snelheid: beslis expliciet hoeveel je je kunt veroorloven te verliezen bij een crash (flush-interval, `AutoFlush`, `Flush(true)`).
3. Async I/O lost dit niet op (het blijft één syscall per call), maar voorkomt dat threads blokkeren; geef in servers de voorkeur aan een achtergrond-writer gevoed door een `Channel`.
4. Het effect hangt af van het bestandssysteem (tmpfs vs SSD vs netwerk), dus meet waar het daadwerkelijk zal draaien.

## Extra credit
Zet `AutoFlush = true` op de `StreamWriter`. Hoeveel van de winst blijft over?

## Ga verder
Schrijf via een `Channel<string>` en één achtergrond-consumer met een gebufferde writer. Wat gebeurt er met durability bij een crash? En met de volgorde?
