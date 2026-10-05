# taskset
> Alleen Linux

**Beantwoordt:** niets uit zichzelf. Het bepaalt **op welke CPU-cores** een programma mag draaien. Gebruik het om metingen stabieler te maken, of om te zien hoe een programma zich gedraagt op een kleinere machine dan de jouwe.

`taskset` zit standaard in Linux (package `util-linux`). Geen installatie nodig.

## Commando's
```bash
taskset -c 0-7 dotnet run -c Release --project labs/<lab>/exercises/<id>       # alleen cores 0 tot 7
taskset -c 0,2 dotnet <...>.dll                                                    # twee specifieke cores
taskset -cp <pid>                                                                  # toon de cores van een draaiend proces
nproc --all; lscpu --extended                                                      # hoeveel cores, en welke is welke
```

## Wanneer het helpt
- **Ruizige getallen.** De web-exercises (Lab 9 en hoger) draaien de server *en* de load generator in één proces. Door het aan een vaste set cores te binden, stopt het OS met hem rond te verplaatsen, zodat herhaalde runs beter overeenkomen.
- **"Werkt op mijn machine."** Jouw laptop heeft misschien 20 cores en productie 2. Draaien op `-c 0,1` laat ongeveer zien wat een container met 2 cores ziet. De runtime dimensioneert de thread pool en de GC op basis van de cores die hij mag gebruiken.
- **Hybride CPU's.** Op Intel-chips met P-cores/E-cores toont `lscpu --extended` welke corenummers welke zijn (verschillende `MAXMHZ`). Vastpinnen op één type geeft veel consistentere tijdmetingen.

## Valkuilen
- **Pin niet op één enkele core** voor tijdmetingen. De achtergrondcompiler van de JIT deelt dan de core met jouw code, en methoden doen er veel langer over om hun geoptimaliseerde versie te bereiken (zie [RUNTIME.md](../RUNTIME.md)).
- De harness kalibreert op de snelheid van deze machine. Pinnen verandert de budgetten niet, alleen hoe stabiel de getallen zijn.
- **Windows:** `start /affinity 3 dotnet ...` in `cmd` (de waarde is een hex-masker: `3` = cores 0 en 1), of zet **Affinity** in Taakbeheer.

## Documentatie
[taskset man page](https://man7.org/linux/man-pages/man1/taskset.1.html)
