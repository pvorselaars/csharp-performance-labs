# L06-03 - Oplossing

## Wat het profiel laat zien
- **Sampling:** de `if`-regel. **`perf stat`:** branch-miss rate ~25–50%.

## Grondoorzaak
Een data-afhankelijke branch op willekeurige data: de predictor zit ongeveer de helft van de tijd fout, en elke misser spoelt de pipeline door.

## Fix
Sorteer één keer (een O(n log n)-kost die je maar één keer betaalt) zodat de 60 passes een perfect voorspelbare branch zien. Een alternatief is een *branchless* formulering (`sum += b & mask`), die de branch helemaal verwijdert (extra credit). De juiste keuze hangt af van hoeveel passes de sort terugverdienen.

## Lessen
1. **Data kan dezelfde code snel of traag maken**: gesorteerde versus ongesorteerde input kan meerdere keren verschillen bij een ongewijzigde loop.
2. Bevestig met de branch-miss-counter, niet alleen met de stopwatch.
3. Sorteren loont alleen als je herhaaldelijk scant; voor één pass heb je liever branchless code of vectorisatie.
4. Nieuwere JIT's zetten simpele `if`'s soms om in conditional moves; de tweeregelige body van deze exercise voorkomt dat met opzet. Kijk naar de disassembly (`DOTNET_JitDisasm`) om te zien wat je daadwerkelijk kreeg.

## Extra credit
Vervang de body door `sum += b >= 128 ? b : 0` in een scratch-kopie. Maakte de JIT hem branchless? Hoe zie je dat?

## Ga verder
Schrijf de branchless versie en zoek het aantal passes waarbij eenmalig sorteren niet meer loont. Kijk daarna naar de JIT-output van beide loops.
