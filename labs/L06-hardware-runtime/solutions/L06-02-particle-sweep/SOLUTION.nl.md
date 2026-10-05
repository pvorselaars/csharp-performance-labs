# L06-02 - Oplossing

## Wat het profiel laat zien
- **Sampling:** de loop-body; verder niets.
- **`perf stat`:** veel meer cache/TLB-misses en stalled cycles voor de reference-array.

## Grondoorzaak
Array van class-instanties = array van pointers. Als de objecten niet in de volgorde liggen waarin je ze bezoekt, is elke access een cache miss (pointer chasing), ook al is de code een simpele loop.

## Fix
Maak `Particle` een `struct` zodat de array de data zelf bevat, aaneengesloten in het geheugen. (Itereer met `ref readonly` over een span om kopiëren te vermijden.) Afweging: structs kopiëren bij toewijzing/doorgave, dus houd ze klein of geef ze door met `in`/`ref`.

## Lessen
1. **Reference-type arrays zijn arrays van pointers**; locality hangt af van waar de allocator (en later, GC-compactie) de objecten heeft neergezet.
2. Struct-arrays / structure-of-arrays zijn de standaardfix voor hot numeric loops (games, simulatie, analytics).
3. De GC compacteert op adresvolgorde, dus vers gealloceerde data ligt vaak *beter* dan de geschudde data van deze exercise, maar langlevende, gemuteerde collecties zakken uit elkaar.
4. Zet niet alles om naar structs: grote structs kopiëren (L06-05) en mutable structs bijten terug. Doe het waar het profiel en de counters het aangeven.

## Extra credit
Verwijder de shuffle in de `Create` van de *trage* versie. Hoe dicht komt de class-array nu in de buurt, en wat zegt dat over de allocator?

## Ga verder
Zet om naar structure-of-arrays (losse `int[] X, Y, ...`) en vectoriseer de som. Welke layout wint als de loop alleen `X` aanraakt?
