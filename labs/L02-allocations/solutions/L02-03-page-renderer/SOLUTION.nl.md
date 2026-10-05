# L02-03 - Oplossing

## Wat het profiel laat zien

- **Harness-output:** gen0 = gen1 = gen2 ≈ 187 per run. Elke collectie is een volledige, de handtekening van LOH-gedreven collecties.
- **allocaties:** 6.000 allocaties van `System.Byte[]` van elk 100.000 bytes, allemaal op de LOH, allemaal dood tegen de volgende iteratie.
- **timeline:** een groot deel van de wall time is GC en het nullen van vers geheugen, niet jouw rasterizer.

## Grondoorzaak
`new byte[100_000]` is ≥ 85.000 bytes, dus die wordt gealloceerd op de **Large Object Heap**. De LOH wordt alleen opgeruimd door gen2-collecties, en een stortvloed aan LOH-allocaties triggert die.
Zesduizend pagina's keer 100 KB is 600 MB aan LOH-allocatie, elke array vers genuld door de runtime, plus ongeveer elke 32 pagina's één volledige GC.

## Fix
Leen de buffer van `ArrayPool<byte>.Shared` en **geef hem terug in `finally`**. Twee regels uit het contract van de pool zijn wat de exercise echt test:
1. Een geleende array mag **langer** zijn dan gevraagd (de shared pool rondt af naar boven, meestal naar een macht van twee): gebruik `AsSpan(0, PageBytes)`, nooit `.Length`.
2. Een geleende array is **niet** genuld; hij kan de data van de vorige lener bevatten. De oorspronkelijke code vertrouwde op een leeg canvas, dus roep `Clear()` aan op de slice (of leen met de clear-on-return-optie van de pool en maak schoon bij het teruggeven).
Zonder die `Clear()` klopt de checksum niet en sluit de harness af met code 2. Dat heb ik precies zo getest.

## Lessen
1. **Als gen0-, gen1- en gen2-aantallen gelijk zijn, verdenk dan de LOH.** Alleen het aantal gealloceerde bytes had je dit niet verteld: een gewone 570 MB zou vooral gen0 zijn geweest.
2. Pooling ruilt allocatie in voor een *correctheidsverplichting*: jij bent nu verantwoordelijk voor het schoonmaken, de grootte en het teruggeven. Een poolbug is stiller dan een traag programma (data van een vorig verzoek dat lekt naar het volgende is ook een beveiligingsprobleem).
3. `ArrayPool<T>.Shared` is thread-safe en goed voor kortlevende buffers. Overweeg voor heel grote of langlevende buffers, of als je een exacte lengte nodig hebt, een eigen pool of `RecyclableMemoryStream`.
4. Geef de buffer terug in `finally`, of laat hem lekken (de pool alloceert dan gewoon een vervanging, dus er crasht niets; je verliest alleen het voordeel).

## Ga verder
Leen in plaats daarvan met `clearArray: true` bij **Return**, en vergelijk. Probeer daarna `stackalloc` voor een grootte die veilig is op de stack, en leg uit waarom 100 KB daar *niet* veilig is.