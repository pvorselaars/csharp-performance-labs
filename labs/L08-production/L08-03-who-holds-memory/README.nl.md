# L08-03 - Wie houdt het geheugen vast?

Het geheugen van de service groeit gestaag (ongeveer 8 MB per seconde). **Vind welk type groeit en wat het in leven houdt, zonder de broncode te lezen.**
(Stop hem voordat hij je RAM opeet: hij draait standaard 120 s, ongeveer 1 GB.)

```powershell
./labs/L08-production/L08-03-who-holds-memory/run.ps1 120         # terminal 1; noteer de pid
dotnet-counters monitor -p <pid> System.Runtime  # groeit de heap? welke generatie?
dotnet-gcdump collect -p <pid> -o a.gcdump       # neem er een op ~10 s ...
dotnet-gcdump collect -p <pid> -o b.gcdump       # ... en een op ~40 s
dotnet-gcdump report a.gcdump | head -30         # topdrachten naar grootte / aantal
dotnet-dump collect -p <pid> -o core.dmp
dotnet-dump analyze core.dmp
> dumpheap -stat          # types naar totale grootte en aantal
> dumpheap -type Session -min 8000   # neem dan een adres ...
> gcroot <address>        # ... en vind wat het roott
```
`dotnet-gcdump` is lichter (triggert eerst een GC, dus toont alleen *live* objecten); `dotnet-dump` is een volledige process dump (groter, bevat ook garbage die nog niet is opgeruimd).
Beantwoord in [QUESTIONS.md](QUESTIONS.md), vergelijk daarna met [ANSWERS.md](ANSWERS.md).
