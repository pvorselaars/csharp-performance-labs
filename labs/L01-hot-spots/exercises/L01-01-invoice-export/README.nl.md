# L01-01 - Factuurexport

*Een gigabyte voor een spreadsheet*

## Symptoom
De nachtelijke job exporteert 5.000 orders naar een CSV-rapport. Het bestand is maar een paar honderd KB, toch
duurt de run een kwart seconde of meer, en de harness meldt **meer dan een gigabyte aan allocaties** en
**honderden GC's**. Er gebeurt ergens veel meer werk dan de omvang van de output rechtvaardigt.

## Doel
Haal beide budgets **zonder de output te veranderen** (de checksum moet nog kloppen):

| Budget | Waarde     |
|---|-----------|
| Mediane tijd | 24 ref-ms |
| Mediaan aantal allocaties | 8 MB      |

## Regels
- Bewerk `Workload.cs`. Bewerk `Program.cs` niet (budgets/checksum staan daar).
- Profileer **voordat** je de code leest voor het antwoord. Schrijf je hypothese eerst in `templates/LAB-LOG.md`.
- Vastgelopen? `HINTS.md`, één hint tegelijk. Oplossing: `labs/L01-hot-spots/solutions/L01-01-invoice-export/` (pas nadat je slaagt).
