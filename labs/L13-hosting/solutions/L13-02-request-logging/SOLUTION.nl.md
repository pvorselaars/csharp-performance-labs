# L13-02 - Oplossing

## Wat het profiel laat zien
- **Timeline:** request-threads geblokkeerd op de lock van de logger; **strace:** duizenden open/write/close.

## Grondoorzaak
Synchrone, per-regel file-I/O onder een globale lock in het logging-pad: request-latency omvat disk-latency en lock-wachttijd.

## Fix
Een asynchrone provider: `Log` enqueuet naar een begrensd channel, één achtergrondthread schrijft naar één buffered `StreamWriter`. Flush bij shutdown, drop en tel wanneer de queue vol is. (Gebruik in echte apps een bewezen async sink: Serilog's async wrapper, OpenTelemetry-exporters, of de console/JSON-logger naar stdout, verzameld door het platform.)

## Lessen
1. **Telemetrie mag geen latency toevoegen of requests laten falen.** Logging, metrics en tracing zitten op het kritieke pad tenzij je ze ontkoppelt.
2. Begrensde queue + drop-and-count wint het van blokkeren wanneer de sink traag is.
3. Log minder, en structureer het: vier regels per request is op schaal duur, zelfs async.
4. Meet de observability-tax (met en zonder) als onderdeel van je performance-budget.

## Extra credit
Wat gebeurt er met logregels die nog in het channel zitten wanneer het proces wordt gekilld? Ontwerp de trade-off.

## Go further
Sample de logging (1 op de 10 requests) en vergelijk p99. Welke informatie verlies je?
