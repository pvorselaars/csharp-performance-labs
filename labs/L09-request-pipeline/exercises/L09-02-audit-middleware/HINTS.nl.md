# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

dotMemory-allocaties gegroepeerd per **call site**; dotTrace sampling. Kijk in het middleware-delegate, niet in het endpoint.
</details>

<details><summary>Hint 2: waar?</summary>

Maak een lijst van alles wat de middleware per request construeert of formatteert: wat is bij elke request *identiek*, en wat gebeurt er met het logbericht als het niveau uitstaat?
</details>

<details><summary>Hint 3: waarom?</summary>

Een `Regex` die per request gebouwd wordt, herhaalt parse- en setup-werk (Lab 1: L01-03); een geïnterpoleerd logbericht wordt geformatteerd voordat de logger het niveau checkt (Lab 5: L05-06). Beide zijn kosten per request die niet van de request afhangen.
</details>
