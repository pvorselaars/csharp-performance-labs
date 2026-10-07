# L11-03 - Oplossing

## Wat het profiel laat zien

- **Pool:** 8/8 in gebruik, veel wachtenden; elke connection brengt ~97% van zijn vasthoudtijd idle door, wachtend op de derde partij.

## Grondoorzaak
Connections worden vastgehouden tijdens een ongerelateerde trage call, waardoor pool-capaciteit ÷ vasthoudtijd de doorvoer begrenst tot ver onder wat de database zou kunnen leveren.

## Fix
Herschik zodat de connection pas na de trage call wordt geleend en direct na de query wordt vrijgegeven; houd leenscopes zo klein mogelijk.

## Lessen
1. **Houd schaarse resources kort vast.** Connections, locks en semaphores moeten alleen het werk omvatten dat ze nodig heeft.
2. Pool-uitputting is een doorvoerplafond (grootte ÷ vasthoudtijd), niet per se een databaseprobleem.
3. Ook gezien bij transacties die worden vastgehouden tijdens HTTP-calls of bedenktijd van de gebruiker.
4. Dezelfde redenering geldt voor thread-pool-threads, `HttpClient`-connecties en semaphore-permits.

## Extra credit
Bereken het doorvoerplafond voor poolgrootte 8, vasthoudtijd 31 ms; daarna voor vasthoudtijd 1 ms. Vergelijk met wat de harness meet.

## Verder gaan
Voeg een timeout toe aan `RentAsync` en geef snel 503 terug als de pool uitgeput is. Wat win je en wat verlies je daarmee?
