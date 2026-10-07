# L06-07 - Warm-up curve

*First Impressions*

## Symptoom
Dezelfde workload duurt **~17 ms de eerste keer en ~6,5 ms elke keer daarna**. In productie is die eerste trage aanroep wat de eerste gebruiker na een deploy ervaart (of het eerste request naar een vers opgeschaalde instance).
De steady-state-cijfers zijn prima; de *eerste* zijn dat niet. Is dit JIT-compilatie, tiering, of iets anders, en wat kun je eraan veranderen?

## Wat deze exercise is
Er is **geen budget** om te halen. De harness warmt in deze modus nooit op. In plaats daarvan draai je de workload koud en bestudeer je de curve:

```bash
dotnet run -c Release --project labs/L06-hardware-runtime/exercises/L06-07-warmup-curve -- --cold --runs 10
```

## Voorspel eerst
Schrijf je voorspellingen in `templates/LAB-LOG.md` **voordat** je iets draait. Draai daarna de workload onder elke instelling en noteer de tijden van run 1 en run 8:

| Instelling (environment variable) | Wat het verandert | Jouw voorspelling | Gemeten run 1 / run 8 |
|---|---|---|---|
| *(standaard)* | tiered compilation + dynamic PGO + ReadyToRun-framework | | |
| `DOTNET_TieredPGO=0` | geen instrumentatie-tier; minder PGO-gestuurde optimalisatie | | |
| `DOTNET_TieredCompilation=0` | elke methode volledig geoptimaliseerd bij de eerste JIT | | |
| `DOTNET_TC_QuickJitForLoops=0` | methodes met loops slaan tier-0 over | | |
| `DOTNET_ReadyToRun=0` | negeer voorgecompileerde framework-code; JIT alles | | |

```bash
DOTNET_TieredPGO=0 dotnet run -c Release --project labs/L06-hardware-runtime/exercises/L06-07-warmup-curve -- --cold --runs 10
```

## Vragen om in je log te beantwoorden
1. Waarom is run 1 trager dan run 2, en waarom lijkt run 2 op run 8?
2. Welke instelling verbeterde **run 1**, en wat kostte dat in **steady state**? Waarom bestaat er geen free lunch?
3. Wanneer zou je een trage eerste request accepteren voor een snellere steady state? Wanneer niet? (Denk aan: batch job versus autoscaling webservice versus CLI-tool.)
4. `DOTNET_ReadyToRun=0` maakt zelfs de latere runs trager. Waarom? Wat zegt dat over hoeveel van de tijd van jouw app framework-code is die *voorgecompileerd* binnenkomt?
