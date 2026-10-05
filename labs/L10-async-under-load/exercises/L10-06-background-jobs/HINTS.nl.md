# Hints (open ze één voor één)

<details><summary>Hint 1: welk tool?</summary>

Tel gelijktijdige achtergrondtaken (`peakConcurrentJobs`) en kijk tijdens de burst naar het aantal threads en de queue length in `dotnet-counters`.
</details>

<details><summary>Hint 2: waar?</summary>

Hoeveel taken kunnen er tegelijk draaien? Wat in de code begrenst dat?
</details>

<details><summary>Hint 3: waarom?</summary>

`Task.Run` per request is een verborgen **onbegrensde wachtrij op de gedeelde thread pool**: niets begrenst de concurrency, en blokkerende taken stelen threads van het afhandelen van requests. Zet een **begrensde wachtrij** (een `Channel`) voor een **vast aantal workers** (een `BackgroundService` in een echte app); accepteer snel, verwerk in een tempo dat het systeem aankan, en pas back-pressure toe of weiger werk als de wachtrij vol raakt.
</details>
