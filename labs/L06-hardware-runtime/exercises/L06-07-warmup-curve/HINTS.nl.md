# Hints (open er één tegelijk)

<details><summary>Hint 1: het mechanisme</summary>

.NET start elke methode in een snel-te-compileren, traag-te-draaien **tier 0** (of gebruikt voorgecompileerde *ReadyToRun*-code), telt aanroepen en loop-iteraties, en compileert hot methodes later opnieuw met de optimaliserende JIT (**tier 1**), met profile-data die onderweg verzameld is (dynamic PGO). Run 1 betaalt voor de trage start; latere runs profiteren van de gepromoveerde code.
</details>

<details><summary>Hint 2: de afweging</summary>

Tiering uitzetten compileert alles direct met de optimizer: geen trage tier-0-code, maar *elke* methode (ook methodes die je nooit twee keer uitvoert) betaalt de volledige JIT-kost bij het opstarten, en je verliest PGO's steady-state-winst.
</details>

<details><summary>Hint 3: waar je naar kijkt</summary>

Gebruik `DOTNET_JitStdOutFile` met `DOTNET_JitDisasmSummary=1` om te zien welke methodes op welke tier en in welke volgorde gecompileerd werden. `dotnet-counters` toont `methods-jitted-count` en `time-in-jit` voor het process.
</details>
