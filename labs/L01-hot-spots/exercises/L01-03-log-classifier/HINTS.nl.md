# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

Sampling-profiel, Release. Klap de call tree onder `LineParser.Parse` uit en kijk *welke onderdelen* van de
regex-machinerie verschijnen. Er zijn twee heel verschillende fases: een patroon voorbereiden, en het uitvoeren.
</details>

<details><summary>Hint 2: waar?</summary>

Frames met namen als parser / writer / char-class / constructor horen bij het *voorbereiden* van het patroon.
Frames met namen als scan / interpreter / match horen bij het *uitvoeren* ervan. Welke groep domineert?
</details>

<details><summary>Hint 3: waarom?</summary>

`new Regex(pattern)` parset de patroontekst en bouwt elke keer opnieuw het matching-programma. Hoe vaak
verandert het patroon tussen aanroepen door? Kijk dan naar het allocatieprofiel: die 380 MB zijn vooral
bijproducten van dat werk.
</details>
