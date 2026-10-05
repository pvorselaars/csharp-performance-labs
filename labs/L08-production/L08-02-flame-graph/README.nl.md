# L08-02 - Flame graph

Een service verbrandt een volledige core. **Vind het hete codepad zonder de broncode te lezen.**

```powershell
./labs/L08-production/L08-02-flame-graph/run.ps1 120                      # terminal 1; noteer de pid
dotnet-trace collect -p <pid> --format Speedscope -o flame --duration 00:00:10    # terminal 2
# open flame.speedscope.json in https://www.speedscope.app  (drag & drop; er wordt niets geüpload)
```
In speedscope: de **Left Heavy**-view toont waar de tijd zit; **Sandwich** toont callers/callees van één functie.
(In Rider kun je ook profilen met dotTrace sampling en de call tree lezen; zelfde antwoord.)

Beantwoord in [QUESTIONS.md](QUESTIONS.md), vergelijk daarna met [ANSWERS.md](ANSWERS.md).

Een kanttekening: de trace bevat ook runtime-frames (GC-poll en thread-frames); zoek naar de `service!`-frames en negeer de rest.
