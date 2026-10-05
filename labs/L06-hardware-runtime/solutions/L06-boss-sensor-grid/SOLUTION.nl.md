# L06-boss - Oplossing

## Wat het profiel laat zien
- Drie afzonderlijke hardware/runtime-effecten; het profiel bestaat uit drie vlakke, onopvallende loops.

## Grondoorzaak
Eén defect uit elk van drie Lab 6-exercises.

## Fix
Doorloop de grid in geheugenvolgorde; maak `Reading` een struct zodat de array de data inline bevat; gebruik de gevectoriseerde `Count` op een span.

## Lessen
1. column-major-doorloop = **L06-01**; array van class-instanties (pointer chasing) = **L06-02**; LINQ `Count` met een lambda = **L06-08**.
2. Geen van deze toont zich als een trage *functie*: ze tonen zich als vlakke loops. Eerst voorspellen, dan meten is de enige manier erin.

## Ga verder
Welke van de drie is na de fix beperkt door geheugenbandbreedte, en welke door instructiedoorvoer? Hoe zou je dat vaststellen?
