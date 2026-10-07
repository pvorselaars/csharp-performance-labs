# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

Begin met een **sampling**-profiel in Release. Open de hot spots / call tree en sorteer op **eigen (self) tijd**,
niet op totale tijd. Kijk ook even naar de harness-output: toegewezen MB en gen0/1/2-aantallen zijn ook aanwijzingen.
</details>

<details><summary>Hint 2: waar?</summary>

`FormatRow` oogt als de verdachte, "dure" methode (`string.Format`, meerdere argumenten). Check hoeveel tijd
hij daadwerkelijk voor zijn rekening neemt. Kijk dan wat er bovenaan self time staat en wie dat aanroept.
</details>

<details><summary>Hint 3: waarom?</summary>

Strings zijn immutable. Wat gebeurt er met de bestaande inhoud elke keer dat je `report += ...` schrijft?
Hoe groeit de totale hoeveelheid kopiëren naarmate het rapport groeit? En wat is er bijzonder aan objecten
groter dan ~85.000 bytes op de .NET-heap?
</details>
