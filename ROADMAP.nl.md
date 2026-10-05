# Roadmap

Wil je gewoon aan de slag, dan heeft de [README](README.md) de snelstart. Dit bestand is de gedetailleerde kaart: wat elk lab leert, wat al gebouwd is versus nog slechts geschetst, en waarom de volgorde is zoals hij is.

Eén reeks van **vijftien labs (0–14)** die één methode delen (meten -> profileren -> hypothese -> één ding veranderen -> opnieuw meten):

- **Labs 0–8, de .NET-runtime:** CPU, allocaties, GC, geheugen, concurrency, hardware, productietooling.
- **Labs 9–14, ASP.NET Core:** dezelfde vaardigheden onder *concurrente belasting*, waar nieuwe faalwijzen opduiken (thread-pool starvation, pool-uitputting, stampedes, retry storms, connection churn).

Voorbij lab 14 staan er nog wat [specialisatietracks](#specialisaties) gepland.

Boeken, artikelen en docs per lab: [docs/READING-LIST.md](docs/READING-LIST.md). Alle projecten targeten **.NET 10**. Elk lab leeft in zijn eigen map onder [`labs/`](labs/), met een eigen README.

**Statuslegenda:**

- ✅ gebouwd en geverifieerd (trage versie faalt zijn budgets, gefixte versie slaagt, dezelfde checksum)
- 📐 ontworpen
- 🛠️ in opbouw
- 💡 idee

| Lab | Thema                                          | Profileervaardigheid                        | Exercises  | Eindbaas                       | Status |
|-----|-----------------------------------------------|---------------------------------------------|------------|-------------------------------|--------|
| 0   | Fundamenten                                   | sampling                                    | 1          | n.v.t.                        | ✅     |
| 1   | Voor de hand liggende hot spots                | sampling, tracing                           | 7          | `L01-boss-order-ledger`        | ✅     |
| 2   | Allocaties & GC-druk                          | allocation profiling                        | 9          | `L02-boss-shipment-manifest`   | ✅     |
| 3   | Lekken & retentie                             | heap snapshots, dominators                  | 6          | `L03-boss-session-gateway`     | ✅     |
| 4   | Async & concurrency                           | timeline view, `dotnet-counters`            | 7          | `L04-boss-notification-hub`    | ✅     |
| 5   | Library stack, één caller                     | SQL/HTTP/IO subsystem views                 | 8          | `L05-boss-order-report`        | ✅     |
| 6   | Hardware- & runtime-effecten                  | `perf stat`, JIT-disassembly                | 9          | `L06-boss-sensor-grid`         | ✅     |
| 7   | Eindbaas-gevechten                            | alles                                       | 4          | (het hele lab)                 | ✅     |
| 8   | Voorbij de IDE (exercises)                    | `dotnet-counters/trace/gcdump/dump`, cgroups | 5 | (L08-05 brengt het samen)      | ✅     |
| 9   | ASP.NET: de request pipeline                  | load driver, allocation profiling           | 5          | `L09-boss-storefront-checkout` | ✅     |
| 10  | ASP.NET: async, threads & de pool onder belasting | counters, timeline view                 | 6          | `L10-boss-async-quotes`        | ✅     |
| 11  | ASP.NET: dataverkeer onder belasting          | EF logging, SQL-aantallen                   | 4          | `L11-boss-orders-service`      | 🛠️     |
| 12  | ASP.NET: caching & uitgaande calls            | counters                                    | 5          | `L12-boss-catalog-service`     | 🛠️     |
| 13  | ASP.NET: hosting, runtime-config, deployment  | keep-alive, logging, GC-instellingen        | 2          | n.v.t.                        | 🛠️     |
| 14  | ASP.NET: productie-capstones                  | alles                                       | 2          | (het hele lab)                 | 🛠️     |

*"Profileervaardigheid" noemt de vaardigheid die je nodig hebt, geen specifiek product; zie [docs/PROFILING-GUIDE.md](docs/PROFILING-GUIDE.md) voor welke tools (Rider, Visual Studio, de gratis CLI-tools, `perf`) je elke vaardigheid geven.*
## Specialisaties

Optionele zijtracks die van de kernlabs aftakken, zodat de kern op vijftien labs blijft staan. Elke track heeft zijn eigen nummering, vereiste en eindbaas-gevecht, en geen enkele is nodig om Labs 0–14 af te ronden. Geplande opzet: `specializations/<track>/{exercises,solutions}`.

| Track                          | Vereiste     | Onderwerpen                                                        | Status |
|--------------------------------|--------------|---------------------------------------------------------------------|--------|
| Databases: RavenDB             | L4, L11      | sessions, N+1, indexes, projections, bulk insert                  | 💡     |
| Observability                  | L9, L13      | OpenTelemetry-overhead, logvolume, metric-cardinaliteit, sampling  | 💡     |
| Serialisatie & wire formats     | L2, L5       | source generators, streaming readers, Protobuf/MessagePack        | 💡     |
| Systeemontwerp-patronen        | L4, L12      | rate limiting, circuit breaker, bulkhead, idempotency, distributed locks, consistent hashing, replicatie/quorum | 💡     |
| Messaging & achtergrondwerk    | L4, L10      | `Channel<T>`, backpressure, batching, consumer lag, poison messages | 💡     |
| Opstarten, AOT & trimming      | L6, L13      | cold start, tiered JIT, ReadyToRun, NativeAOT                      | 💡     |
| gRPC & HTTP/2-3                | L9           | streaming, multiplexing, header-compressie, connection/stream limits | 💡     |

**Hoe je erdoorheen werkt:** op volgorde, 0 tot 14. Elk lab eindigt met een **eindbaas-gevecht**: een vermomde combinatie van de defecten uit dat lab, zonder hints per defect (Labs 7 en 14 *zijn* eindbaas-labs). Labs 9–14 veronderstellen Labs 1–4; wil je eerder aan ASP.NET beginnen, dan kun je Labs 9–10 direct na Lab 4 doen en Labs 11–12 na Lab 5. Lab 6 staat los van Labs 3–5. Elk lab heeft een **meesterschap-checkpoint**: iets om *zonder aantekeningen* te doen voordat je verdergaat.