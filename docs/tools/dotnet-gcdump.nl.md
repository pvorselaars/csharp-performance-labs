# dotnet-gcdump

**Beantwoordt:** wat staat er op de managed heap, per type, en wat houdt het daar vast? Een gcdump bevat de object-*graaf* (types, aantallen, groottes, referenties), niet de objectinhoud, dus de bestanden blijven klein.

## Commando's
```bash
dotnet-gcdump collect -n <id> -o a.gcdump              # neem één snapshot
dotnet-gcdump report a.gcdump | head -30               # grootste types op grootte, in de terminal
```

Om een lek te vinden, neem **twee** snapshots met enige tijd ertussen, terwijl het proces in de tussentijd hetzelfde werk doet:
```bash
dotnet-gcdump collect -n <id> -o a.gcdump
# ...wacht 20-30 seconden...
dotnet-gcdump collect -n <id> -o b.gcdump
```
Open ze daarna allebei in Visual Studio (**Compare to** een ander snapshot) of PerfView en kijk wat er *gegroeid* is. Rider/dotMemory kan ze ook importeren.

## Hoe je het leest
```
      1,397,379  GC Heap bytes
         15,849  GC Heap objects

   Object Bytes     Count  Type
        352,692         1  System.String (Bytes > 100K)
         40,024         1  InvoiceExport.Order[] (Bytes > 10K)
```
- **Grootte en aantal zijn allebei aanwijzingen.** Eén enorm object is een buffer of een grote array. Miljoenen kleine is een collectie die blijft groeien.
- `System.String`, `Byte[]` en `Object[]` staan vrijwel altijd bovenaan. Zoek naar **jouw** types, en naar de collecties die ze vasthouden.
- De grootte-kolom is de eigen grootte van elk object (shallow), niet alles waar het naar verwijst. Een `Dictionary` die 80 MB vasthoudt kan als een paar KB verschijnen. Een viewer met retention paths (VS, PerfView, dotMemory) toont wat wat vasthoudt.

## Valkuilen
- **Een gcdump nemen draait een volledige, blokkerende GC.** Dat is prima in dit lab. In productie pauzeert het proces zo lang als de collectie duurt.
- Een gcdump toont alleen **levende** objecten, omdat de GC net gedraaid heeft. Is het geheugengebruik hoog maar de gcdump klein, dan is het geheugen garbage dat wacht op collectie, native geheugen, of vrije ruimte binnen de GC-heap. Dat is geen managed lek.
- Het heeft geen inhoud: je kunt zien *dat* er 40.000 `Session`-objecten zijn, maar niet hun veldwaarden. Gebruik daarvoor [dotnet-dump](dotnet-dump.md).

## Documentatie
[dotnet-gcdump (Microsoft Learn)](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/dotnet-gcdump)
