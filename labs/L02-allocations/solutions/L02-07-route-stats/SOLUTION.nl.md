# L02-07 - Oplossing

## Wat het profiel laat zien
- **allocaties:** ~1.000.000 korte `System.String`-objecten (60–80 bytes elk) van `DefaultInterpolatedStringHandler`; de dictionary zelf is maar een klein deel.
- **tracing:** `string.GetHashCode` / `Marvin`-hashing en `string.Equals` scoren hoog: dezelfde teksten steeds opnieuw hashen.

## Grondoorzaak
De dictionary-key is een **string die per request wordt opgebouwd** via interpolatie (`"{tenant}:{route}:{status}"`): een allocatie en een volledige string-hash per event, plus een tweede lookup. Het terugleren van de tabel vereist `Split` en `int.Parse` om de formattering ongedaan te maken: de gestructureerde data werd platgeslagen tot een string en weer geparsed.

## Fix
Gebruik een struct-key: `readonly record struct Key(int Tenant, int Route, int Status)`. Een record struct krijgt gratis value equality, een goede `GetHashCode` en `IEquatable<Key>`, dus `Dictionary` vergelijkt hem zonder boxing en er bestaat geen heap-object per event. Het terugleren heeft geen parsen nodig: de key heeft zijn velden al.

## Lessen
1. **Sla gestructureerde data niet plat tot strings om als key te gebruiken.** Je betaalt voor het alloceren, hashen, vergelijken, en later parsen ervan.
2. Value-type keys hebben `IEquatable<T>` nodig; anders valt `EqualityComparer<T>.Default` terug op de object-gebaseerde vergelijking en **boxt hij bij elke call.** `record struct` en `ValueTuple` implementeren dat voor je.
3. De resterende 1,9 MB zijn de eigen arrays van de dictionary terwijl die groeit; `new Dictionary<Key,int>(capacity)` snoeit dat.
4. De budgetgate zit op *bytes*: de kapotte struct-versie was snel (29 ms) maar zou het allocatiebudget nog steeds niet halen. Dat is precies het punt van beide gaten.

## Ga verder
Pak de drie waarden in één `long`-key. Sneller of trager dan de `record struct`? Wat zou je verliezen (leesbaarheid, uitbreidbaarheid)? Gebruik daarna `GetAlternateLookup` (als je doelframework dat heeft) voor string-lookups zonder allocatie.