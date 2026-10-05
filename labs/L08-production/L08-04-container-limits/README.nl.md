# L08-04 - Containerlimieten

Dezelfde service (150 MB live data plus een stroom kortlevende garbage) draait onder verschillende **geheugenlimieten**. Voorspel, en observeer daarna:

```powershell
./labs/L08-production/L08-04-container-limits/run.ps1 none
./labs/L08-production/L08-04-container-limits/run.ps1 260M
./labs/L08-production/L08-04-container-limits/run.ps1 220M
./labs/L08-production/L08-04-container-limits/run.ps1 200M
./labs/L08-production/L08-04-container-limits/run.ps1 220M DOTNET_GCHeapHardLimit=0xC800000      # 200 MB expliciete heap-cap
./labs/L08-production/L08-04-container-limits/run.ps1 220M DOTNET_GCConserveMemory=9
```
De service print elke 2 s zijn GC-aantallen, heap-grootte en **het geheugenlimiet dat de GC denkt te hebben**, en aan het eind het totale verrichte werk ("allocaties", als doorvoer-proxy).
Vul [QUESTIONS.md](QUESTIONS.md) in, vergelijk daarna met [ANSWERS.md](ANSWERS.md).

**Vereisten:** Docker of Podman (Docker Desktop op Windows/macOS). Het script bouwt bij de eerste run een kleine Linux-image vanuit [Dockerfile](Dockerfile) (de .NET SDK/runtime-images worden eenmalig gepulld) en past de limiet toe met `--memory` (plus `--memory-swap` gelijk daaraan, dus geen swap), wat precies zo'n cgroup-limiet is als in Kubernetes. Het werkt identiek op Windows, macOS en Linux; `none` draait dezelfde container zonder limiet. Voor dit lab is geen voorafgaande `build-all.ps1` nodig.

Handmatig equivalent: `docker build -t perflab-l08-04 .` gevolgd door `docker run --rm --memory=220m --memory-swap=220m -e DOTNET_GCConserveMemory=9 perflab-l08-04`.
