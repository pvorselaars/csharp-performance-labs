# L08-01 - Triage

Vijf kopieën van één service, elk in een andere toestand (scenario's `a` tot `e`). Jouw taak: zeg voor elke kopie **welke resource het probleem is**: CPU, garbage collection, de thread pool, een lock, of geen (gezond). Je mag alleen `dotnet-counters` gebruiken.

```powershell
./labs/L08-production/L08-01-triage/run.ps1 a 120            # terminal 1: print zijn pid
dotnet-counters ps                          # terminal 2 (zoek de pid)
dotnet-counters monitor -p <pid> --showDeltas System.Runtime
# of, om getallen te houden die je kunt optellen:
dotnet-counters collect -p <pid> --counters System.Runtime --format csv -o a.csv --refresh-interval 1 --duration 00:00:10
```

Vul [QUESTIONS.md](QUESTIONS.md) in. Vergelijk daarna met [ANSWERS.md](ANSWERS.md).
