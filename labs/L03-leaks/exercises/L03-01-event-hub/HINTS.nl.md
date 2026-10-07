# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

dotMemory: snapshot vooraf, draaien, snapshot na een geforceerde GC, **Compare**. Sorteer op *new objects* of *retained size*. Welk type groeide met ongeveer 2.000?
</details>

<details><summary>Hint 2: waar?</summary>

Open het retentiepad (*Key retention paths* / *Dominators*) voor één overlevende `Widget`. Lees het van de GC-root naar beneden. Wat is het eerste dat *geen* lokale variabele van je is?
</details>

<details><summary>Hint 3: waarom?</summary>

Een `Hub`-event is een delegate-lijst; `+=` slaat een delegate op waarvan het target de subscriber is. Een publisher die zijn subscribers overleeft, houdt ze allemaal in leven. Wat moet een subscriber doen als hij klaar is, en hoe zorg je dat dat moeilijk te vergeten is?
</details>
