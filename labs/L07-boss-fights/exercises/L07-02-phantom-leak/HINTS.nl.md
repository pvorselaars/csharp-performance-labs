# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

Vergelijk twee counters over tijd: `dotnet-counters` **`gc-heap-size`** (plat) vs **`working-set`** (oplopend). Als die het niet met elkaar eens zijn, zit het geheugen niet in de managed heap. OS-tools (`pmap -x <pid>`, `/proc/<pid>/smaps`) laten zien waar de groei wél zit.
</details>

<details><summary>Hint 2: waar?</summary>

Een managed leak toont zich als reachable objects; dit niet. Wat in de code wijst geheugen toe dat de garbage collector *niet* beheert, en wie is verantwoordelijk om het terug te geven?
</details>

<details><summary>Hint 3: waarom?</summary>

De GC ruimt het kleine managed wrapper-object op, maar weet niets van het blok van 1 MB waar het naar wees. Unmanaged geheugen heeft een expliciete release nodig: `IDisposable` (deterministisch) plus een finalizer of `SafeHandle` als vangnet.
</details>
