# L04-06 - Oplossing

## Wat het profiel laat zien

- **Timeline:** ~50% van de tijd wachtend; lock-contention hoog.
- **Sampling:** tijd binnen het trage pad van `Monitor.Enter`/`TryEnter`.

## Grondoorzaak
Eén hete lock op een pad dat vooral uit reads bestaat: zelfs een piepklein critical section wordt een serialisatiepunt zodra veel threads hem achter elkaar nemen (lock convoy, cache-line ping-pong op het lock-word).

## Fix
Copy-on-write snapshot: lezers lezen een `volatile`-referentie naar een dictionary die als immutable behandeld wordt (lock-free); de zeldzame schrijver kopieert, wijzigt, en wisselt de referentie om onder een lock die alleen voor schrijvers geldt. Kosten: elke schrijfactie kopieert de hele map: prima voor 1.000 entries en een paar schrijfacties; verkeerd voor grote maps met veel schrijfacties.

## Lessen
1. **Data die vooral gelezen wordt, wil lock-free reads.** Copy-on-write, `ImmutableDictionary`, of `FrozenDictionary` (herbouwd bij wijziging) zijn de gebruikelijke middelen.
2. `ReaderWriterLockSlim` is *geen* automatische fix: bij heel korte critical sections kan de eigen overhead ervan erger zijn dan een gewone `lock`. Meet het (extra credit).
3. Het schrijfpad betaalt de rekening: ken het schrijftempo en de objectgrootte voordat je kiest.
4. Publicatie heeft `volatile` nodig (of `Volatile.Write`/`Interlocked.Exchange`) zodat lezers een volledig opgebouwd object zien.

## Extra credit
Probeer in plaats daarvan `ReaderWriterLockSlim`. Verslaat het de `lock`? Verslaat het copy-on-write? Leg de volgorde uit die je waarneemt.

## Go further
Gebruik `FrozenDictionary` die bij elke schrijfactie herbouwd wordt, en vergelijk de leessnelheid. Wat als schrijfacties 10% van de operaties zouden zijn in plaats van 0,005%?
