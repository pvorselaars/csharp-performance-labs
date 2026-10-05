# L08-05 - Een performance-gate in CI

De budgetten van de harness zijn al een gate. In dit lab maak je ze **automatisch**, en bepaal je wat de moeite waard is om te gaten en hoe je dat betrouwbaar houdt.

## Deel 1: draai de gate
```powershell
./scripts/perf-gate.ps1 L01         # bouwt, controleert dan: elke oplossing SLAAGT, elke exercise FAALT
```
Een groene run betekent dat de budgetten *nog steeds* trage code van gefixte code scheiden. Een `SURPRISE` betekent óf een regressie in een oplossing, óf een budget dat niet meer bijt.

## Deel 2: breek met opzet iets
Kies een oplossing en introduceer een regressie (voorbeelden hieronder), draai `./scripts/perf-gate.ps1 <prefix>`, en bevestig dat hij rood kleurt. Zeg daarna *welk budget* het opving en of dat het juiste is.
- `labs/L02-allocations/solutions/L02-05-price-lookup`: verander `ValueTask<decimal>` terug naar `async Task<decimal>` (allocatie-gate).
- `labs/L04-async-concurrency/solutions/L04-04-config-cache`: verander `Lazy<int>` terug naar een gewone waarde (CPU- en `factoryCalls`-gates).
- `labs/L01-hot-spots/solutions/L01-03-log-classifier`: construeer de `Regex` weer per aanroep.
- Een **stille** regressie: in `labs/L02-allocations/solutions/L02-03-page-renderer`, verwijder de `Clear()`. Welke gate vangt dit op? (Correctheid, exitcode 2.)

## Deel 3: ontwerp de gate
Beantwoord in [QUESTIONS.md](QUESTIONS.md): wat je gate, hoe je budgetten afleidt van een SLO, en hoe je omgaat met ruis.

Vergelijk met [ANSWERS.md](ANSWERS.md). Zie ook [.github/workflows/perf-gate.yml](../../../.github/workflows/perf-gate.yml) voor een workflow-voorbeeld.
