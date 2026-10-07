# L01-01 - Oplossing

## Wat het profiel laat zien
- Sampling: self time concentreert zich in string-concatenatie en de memory copy daaronder
  (`String.Concat` > `Buffer.Memmove`). `FormatRow`, de "duur ogende" methode, is maar een klein deel.
- Harness-output: ~1,27 GB toegewezen om een bestand van een paar honderd KB te produceren, en **389 gen0/gen1/gen2-GC's**
  (hetzelfde aantal drie keer: elke GC was een volledige gen2-collectie).

## Grondoorzaak
`report += row` maakt elke iteratie een gloednieuwe string en kopieert daarin het hele bestaande rapport.
De kopieerkosten per rij groeien mee met het rapport, dus het totale werk is **O(n²)**: 5.000 rijen ≈ 1,2 GB aan kopiëren.
Zodra de string de ~85.000 bytes passeert, belandt hij op de **Large Object Heap**, en LOH-allocatiedruk
triggert gen2-collecties: dat zijn die 389 volledige GC's.

## Fix
Eén `StringBuilder`, vooraf gedimensioneerd (`orders.Count * 64 + 64`), die velden direct toevoegt (geen
tijdelijke string per rij). `string.Join`, of `string.Create`, of rechtstreeks naar een `TextWriter`/stream
schrijven zou ook werken.

## Take-aways
1. **Self time vs. total time.** De methode die *lijkt* duur (`FormatRow`) was het niet; de goedkoop ogende `+=` wel.
2. Tijd en allocaties zijn twee kanten van hetzelfde probleem. Het allocatiegetal is ook een *deterministische*
   regressietest: het schommelt niet zoals kloktijd.
3. `a + b + c` in één expressie is prima (de compiler genereert één `Concat`). Het is `+=` **in een loop** dat pijn doet.
4. De LOH-grens van 85.000 bytes is het onthouden waard; je komt hem opnieuw tegen in Lab 2 en Lab 7.

## Verder
- Overgebleven allocaties: de `ToString("F2")`-aanroepen per rij. Gebruik `ISpanFormattable.TryFormat` naar een
  `Span<char>` en laat ze vervallen (een Lab 2-techniek).
- De output is cultuurgevoelig in een normale app; gebruik `CultureInfo.InvariantCulture` voor machineleesbare CSV.
