# L01-07 - Oplossing

## Wat het profiel laat zien
> Illustratief: profiler-weergaven zijn wat de code impliceert (geen daadwerkelijke profiler-opname).

- **Sampling:** `RuntimeType.GetProperty`, `RuntimePropertyInfo.GetValue`/`Invoke`, argumentcontrole; **allocaties:** geboxte `decimal`/`int`, `object[]`.

## Grondoorzaak
Reflection per item: een property-lookup en een geboxte, laat-gebonden lezing bij elk van de 600.000 reads.

## Fix
Gebruik de properties van de klasse direct, in één enkele loop.

## Take-aways
1. Reflection alloceert (boxing, argument-arrays) naast dat het CPU kost.
2. Maak het niet onnodig ingewikkeld; directe member-toegang is veel sneller dan reflection.
3. Voeg de drie loops samen tot één loop.

## Verder
Bouw de getter generiek met `Delegate.CreateDelegate` vanuit de `PropertyInfo`. Vergelijk met de `switch` en met compiled expressions.
