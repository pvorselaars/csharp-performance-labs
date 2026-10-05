# L06-06 - Oplossing

## Wat het profiel laat zien
- **Disassembly:** `call [rax+…]` (indirecte call) per element in de trage loop; geïnlinede vermenigvuldigingen in de fix.
- **`perf stat`:** veel `branch-misses` op de indirecte branch (traag).

## Grondoorzaak
Interface-dispatch over een heterogene sequentie in willekeurige volgorde: een onvoorspelbare indirecte call per element die bovendien inlining blokkeert.

## Fix
Bewaar de data gegroepeerd per concreet type (drie arrays) en loop over elk apart. Elke aanroep verdwijnt (geïnlinede veld-aritmetiek). Andere fixes: de interface-array sorteren/groeperen op type; een `switch` op een tag-veld; generics met struct-constraints (`where T : struct, IShape`) zodat de JIT per type specialiseert.

## Lessen
1. **Virtual/interface-calls zijn goedkoop als ze voorspelbaar zijn en duur als dat niet zo is**; de kost is branch-misprediction plus verloren inlining.
2. `sealed` helpt alleen als de JIT het exacte type kan zien; hier kan dat niet (het statische type is de interface).
3. Groeperen per type (of een struct-generic aanpak) is een standaard data-oriented fix voor polymorfe hot loops.
4. Dynamic PGO (standaard aan sinds .NET 8) devirtualiseert het *dominante* type per call site; het kan een gelijkmatige mix niet redden.

## Extra credit
Verander de mix naar 95% `Rect`. Wat doet de trage versie, en waarom? (Denk aan dynamic PGO.)

## Ga verder
Houd de interface-array, maar sorteer hem één keer op type; is dat genoeg? Probeer daarna de generic struct-constraint-aanpak.
