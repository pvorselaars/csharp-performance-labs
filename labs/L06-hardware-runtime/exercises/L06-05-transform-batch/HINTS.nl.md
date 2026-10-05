# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

Kijk naar de **disassembly**: `DOTNET_JitDisasm="Run"` (of BenchmarkDotNet's `[DisassemblyDiagnoser]`). Zoek naar een grote block copy (`vmovdqu`-reeksen / `rep movs`) *vóór elke aanroep* van `Score`.
</details>

<details><summary>Hint 2: waar?</summary>

De struct zit in een `readonly`-veld. De compiler moet garanderen dat het veld niet verandert door de aanroep. Wat moet hij daarvoor aannemen over `Score()`?
</details>

<details><summary>Hint 3: waarom?</summary>

Als een methode wordt aangeroepen op een `readonly`-veld en de compiler niet kan bewijzen dat de methode de struct ongemoeid laat, roept hij de methode aan op een **defensieve kopie**: een kopie van 512 bytes per aanroep. Door de struct `readonly` te maken (of alleen de methode `readonly`) vertel je de compiler dat het veilig is, zodat er geen kopie nodig is.
</details>
