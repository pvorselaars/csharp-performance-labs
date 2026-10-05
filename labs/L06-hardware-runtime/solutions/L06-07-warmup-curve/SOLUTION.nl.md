# L06-07 - Oplossing (gemeten op één machine)

> Onderstaande cijfers zijn gemeten (20-core Linux-machine, .NET 10, Release, `--cold --runs 8`). Die van jou zullen afwijken; de *vorm* is waar je op moet letten.

## Gemeten
| Instelling | run 1 (ms) | runs 2–8 (ms) |
|---|---|---|
| standaard | 17,0 | 6,5–6,7 |
| `DOTNET_TieredPGO=0` | 14,5 | 7,4–7,9 |
| `DOTNET_TieredCompilation=0` | 14,1 | ~7,1 |
| `DOTNET_TC_QuickJitForLoops=0` | 12,8 | ~7,0 |
| `DOTNET_ReadyToRun=0` | 25,5 | ~13,3 |
| standaard + `[AggressiveOptimization]` op `Checksum` (de `Workload.cs` van deze map) | 16,6 | 6,2–6,5 |

## Wat het laat zien
1. **Run 1 is ~2,5x de steady state**, vooral compilatie en tier-0-executie. Run 2 lijkt hier al op run 8 omdat de hot loop snel gepromoveerd wordt (on-stack replacement voor de lange loop, daarna tier 1).
2. **Tiering uit (`TieredCompilation=0`) of loops die tier 0 overslaan (`QuickJitForLoops=0`)** verbeteren run 1 met ~15–25%, maar maken de steady state ~8% *trager* dan standaard: je geeft profile-guided optimisation op. PGO alleen uitzetten heeft dezelfde steady-state-kost.
3. **ReadyToRun uitschakelen** verdubbelt ruwweg elke run (in dit venster): een groot deel van de tijd zat in framework-code (`LINQ`, `Dictionary`) die normaal voorgecompileerd binnenkomt en daarna door tiering verbeterd wordt. Dit is waarom je **eigen** code publiceren met ReadyToRun de latency van het eerste request helpt.
4. **`[MethodImpl(AggressiveOptimization)]` op de hot methode maakte hier geen meetbaar verschil voor run 1**, omdat die methode maar een deel van de kost is. De les: meet voordat je een attribuut toevoegt. (Het schakelt ook tiering en PGO voor die methode uit, dus het kan de steady state juist *schaden*.)

## Kiezen (geen free lunch)
| Situatie | Voorkeur |
|---|---|
| Langlopende service, latency-gevoelige eerste requests na deploy | ReadyToRun (publiceren) **plus** een warm-up-fase voordat hij bij de load balancer aansluit; houd tiering en PGO aan |
| Kortlevende CLI / function | ReadyToRun of Native AOT; accepteer een lagere piek |
| Throughput-batchjob | standaardinstellingen (tiering + PGO) |
| Micro-hot methode die al bij aanroep 1 snel moet zijn | `AggressiveOptimization`, na het meten |

## Extra credit
Compileer de app met ReadyToRun (`dotnet publish -c Release -r linux-x64 --self-contained false -p:PublishReadyToRun=true`) en vergelijk run 1. Probeer daarna een *warm-up-aanroep* bij het opstarten toe te voegen en meet wat het eerste "echte" request ziet.
