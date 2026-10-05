# L01-02 - Oplossing

## Wat het profiel laat zien
Alleen tijd zat boven budget, dus memory tools waren de verkeerde keuze. In een sampling-profiel zijn de
top-frames de lambda binnen `Any`, `string.Equals` met `OrdinalIgnoreCase`, en `Enumerable.Any`, allemaal
aangeroepen vanuit `ContactImporter.Import`. Niets is op zichzelf traag; er zijn gewoon enorm veel aanroepen
(orde van 10⁸ vergelijkingen).

## Grondoorzaak
`seen` is een `List<string>` die voor elke rij lineair doorzocht wordt. Werk = rijen x (gemiddelde grootte van
`seen`) = **O(n²)**. Het bleef verborgen bij kleine aantallen: 2.000 rijen is zo'n 100x minder vergelijkingen dan 20.000.

## Fix
`HashSet<string>(StringComparer.OrdinalIgnoreCase)` en `if (!seen.Add(email)) continue;`: één hash-lookup per
rij, en `Add` doet tegelijk dienst als "al gezien?"-test (geen aparte `Contains` + `Add`).
De comparer behoudt exact de semantiek van het oude `string.Equals(..., OrdinalIgnoreCase)`.

## Take-aways
1. Welk budget faalde vertelde je welk tool je nodig had. CPU-bound met vlak geheugen = sampling.
2. Complexiteitsbugs zijn onzichtbaar tot N groeit. Vraag jezelf bij het reviewen van code af: "wat is N in productie?"
3. De oude code alloceerde ook een closure per rij (de lambda vangt `email`). Dat verdween als bijeffect mee.

## Verder / afwegingen
- Een `HashSet` kost geheugen (~tientallen bytes per entry) en heeft hashingkosten per lookup. Voor N ≈ 10
  is een `List`-scan vaak *sneller*. Meet op de realistische N, niet de theoretische.
- Had je zowel invoegvolgorde *als* lookups nodig, dan hield je de resultaat-`List` plus de `HashSet` aan, zoals hier.
