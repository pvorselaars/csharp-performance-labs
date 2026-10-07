# L03-06 - Oplossing

## Wat het profiel laat zien
- **snapshots:** static `List<Func<int>>` → `Func<int>`-delegate → closure-display-class → `byte[20000]`.

## Grondoorzaak
De closure van een langlevende delegate houdt een grote local (`report`) vast waarvan de callback maar één element nodig had.

## Fix
Kopieer de benodigde waarde naar een kleine local en leg die vast. Ook: meld callbacks af zodra hun eigenaar klaar is (L03-01), en geef de voorkeur aan `static` lambdas met expliciete state om per ongeluk vastleggen te voorkomen.

## Take-aways
1. **Een closure houdt alles wat hij vastlegt in leven zolang de delegate leeft.**
2. Langlevende delegates (events, registries, timers, caches) zijn waar dit toeslaat.
3. Leg minimale waarden vast; een `static` lambda kan helemaal niets vastleggen, waardoor ongelukken een compile-fout worden.
4. Retentiepaden via `<>c__DisplayClass` zijn de fingerprint.

## Extra credit
Wat als de lambda `this` had vastgelegd in plaats van `report`? Wat zou er dan behouden blijven?

## Go further
Herschrijf met een `static` lambda die de waarde als argument meekrijgt (`Func<int,int>` + state).
