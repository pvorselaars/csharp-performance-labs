# L02-02 - Oplossing

## Wat het profiel laat zien

- **allocaties:** verspreid over `string[]`, `string`, `Enumerable+SelectArrayIterator`, `<>c__DisplayClass` (de closure), en delegate-instanties. Geen enkele schuldige; dat is hoe de dood door duizend sneetjes eruitziet.
- **traces:** `String.Split`, `String.Trim`, `String.ToLowerInvariant` en de LINQ-iterators staan bovenaan qua self time.

## Grondoorzaak
Parsen door een string in nieuwe strings te knippen. `Split` alloceert een array plus een string per veld; `Trim`, `ToUpperInvariant` en `ToLowerInvariant` alloceren opnieuw;
de LINQ-keten alloceert iterators, en de lambda vangt `disabledFlags`, dus die alloceert ook een closure en delegates bij elke call. Per regel is dat ongeveer 530 bytes voor één klein struct-resultaat.

## Fix
Slice, kopieer niet. Parse over `ReadOnlySpan<char>` met `IndexOf` en slicing, vergelijk met `Equals(..., StringComparison.OrdinalIgnoreCase)` in plaats van eerst naar lowercase te converteren, en gebruik de `int.Parse`/`decimal.Parse`-overloads die spans accepteren.
Til de disabled-flags-set uit het per-regel-pad: maak er **één keer** een `LineFlags`-masker van en pas dat toe met `flags &= ~disabled`.

## Lessen
1. **Zero allocation is haalbaar bij parsen** wanneer het resultaat een struct is en je de tussentijdse strings nooit nodig hebt.
2. Twee verschillende soorten fixes hier: *niet kopiëren* (spans) en *niet herbouwen wat niet verandert* (het masker, één keer berekend, niet per regel).
3. `static` lambdas (`static x => ...`) laten de compiler captures weigeren, een goedkope manier om closure-allocaties uit hot code te houden.
4. De span-versie is langer en lastiger te lezen. Besteed die complexiteit alleen waar de profiler zegt dat het parsen heet is.

## Extra credit
Jouw parser moet `" eu "`, `"APAC"`, `"Gift"`, `" fragile"` en onbekende flags nog steeds hetzelfde behandelen als voorheen, en regels met
een verkeerd aantal velden afwijzen. Voeg drie edge-case-regels toe aan de input in een scratch-kopie en controleer of jouw versie en het origineel het eens zijn. Hoe zit het met een leeg flags-veld?

## Ga verder
Probeer `MemoryExtensions.Split` voor het algemene geval (check wat je doelframework biedt). Probeer ook `string.Create` en `SearchValues<char>`. Verslaat een van beide de handgeschreven scanner?