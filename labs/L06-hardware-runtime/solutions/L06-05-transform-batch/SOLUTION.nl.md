# L06-05 - Oplossing

## Wat het profiel laat zien
- **Sampling:** tijd in `Run` en de copy-prologue; **disassembly:** een 512-byte block copy vóór elke `Score()`-aanroep in de trage versie, geen enkele in de fix.

## Grondoorzaak
Een instance-methode op een *mutable* struct, aangeroepen via een `readonly`-veld, dwingt de compiler om een defensieve kopie van de hele 512-byte struct te maken bij elke aanroep.

## Fix
Maak de struct `readonly` (de compiler verifieert dan immutability en elideert de kopieën). Of markeer alleen de methode `readonly`. Of geef de struct door met `in`/`ref readonly` aan methoden die hem accepteren. (De constructor hier gebruikt `Unsafe.AsRef` alleen om een `InlineArray` binnen een readonly struct te vullen; een gewone struct met velden zou dat niet nodig hebben.)

## Lessen
1. **Grote structs maken verborgen kopieën**: doorgeven by value, `foreach` over struct-collecties, en methodeaanroepen via `readonly`-velden kopiëren allemaal.
2. `readonly struct` is bijna gratis correctheid *en* snelheid: kies het standaard voor value types.
3. Analyzers/IDE-hints signaleren defensieve kopieën (IDE0250, 'struct can be made readonly'); de disassembly bewijst ze.
4. Vuistregel: structs tot ~16 bytes kopiëren goedkoop; daarboven denk je aan `in`/`ref` of maak je er een class van. Onder ~128 bytes is het effect klein (gemeten: 1,2× bij 128 B en 3,5× bij 512 B).

## Extra credit
Verwijder `[MethodImpl(NoInlining)]` van `Score`. Krimpt het verschil tussen de twee versies? Wat zegt dat over inlining en copy-eliminatie?

## Ga verder
Houd de mutable struct, maar markeer alleen `Score()` als `readonly`. Zelfde snelheidswinst? Geef de struct daarna met `in` door aan een static methode en vergelijk.
