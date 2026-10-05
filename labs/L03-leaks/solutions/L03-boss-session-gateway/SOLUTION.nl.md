# L03-boss - Oplossing

## Wat het profiel laat zien
- Drie onafhankelijke retentie-roots: de delegate-lijst van een langlevend event, een onbegrensde profile-dictionary, en een callback-lijst waarvan de closures een scratch-array vastleggen.

## Grondoorzaak
Eén defect uit drie Lab 3-exercises.

## Fix
Afmelden (`IDisposable`); de profile-cache begrenzen (oudste-eerst eviction, ruim boven de lookback van 50 entries); alleen de benodigde waarde in de callback vastleggen.

## Take-aways
1. **Defect > bron:** views die zich nooit afmelden bij `Presence` = **L03-01**; een altijd groeiende `Dictionary` = **L03-02**; closures die een grote local vastleggen = **L03-06**.
2. In een snapshot-compare heeft elke root zijn eigen retentiepad: heb je alle drie gevonden door naar *paden* te kijken, niet alleen naar groottes?
3. Welke van de drie was het grootst? Welke was het lastigst te vinden?

## Extra credit
Breek je eigen fix: wat gebeurt er als de lookback groeit naar 500 entries?

## Go further
Registreer de callbacks met een unsubscribe-token en verwijder ze zodra de sessie eindigt.
