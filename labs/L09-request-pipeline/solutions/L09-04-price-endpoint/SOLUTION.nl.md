# L09-04 - Oplossing

## Wat het profiel laat zien
- **Allocaties:** ~600 KB per request, bijna allemaal uit de constructor van `PriceCatalog`: resultaten van `string.Split` (één `string[]` en twee strings per CSV-regel), en een `Dictionary<int, decimal>` die tot 2.000 entries toe wordt vergroot.
- **Call count:** `PriceCatalog..ctor` draait één keer per request: 1.500 keer per run, voor data die nooit verandert.

## Grondoorzaak
`PriceCatalog` is geregistreerd als **transient**, dus de container maakt er elke keer een nieuwe van als er iets om vraagt. `PricingService` wordt voor elke request opgelost, en die hangt af van `PriceCatalog`, dus elke request parst het hele prijsbestand naar een nieuwe dictionary, gebruikt één entry, en gooit hem weg.

Er lijkt niets mis met de code: `AddTransient` is wat de meeste voorbeelden gebruiken, en de constructor "laadt gewoon de prijzen". De kosten vallen pas op zodra je merkt *hoe vaak* die constructor draait.

## Fix
`builder.Services.AddSingleton<PriceCatalog>();` Eén woord. De catalogus wordt bij het eerste gebruik gebouwd en gedeeld door elke request. Dat is veilig omdat hij na constructie alleen-lezen is (een `Dictionary` is veilig voor gelijktijdige reads zolang niemand schrijft). `PricingService` mag transient blijven: het is goedkoop, en een transient mag van een singleton afhangen.

## Lessen
1. **Match de levensduur met de kosten en de state.** Duur om te bouwen en immutable -> singleton. Goedkoop en stateless -> transient (of singleton). Houdt state per request vast (een `DbContext`, de huidige gebruiker) -> scoped.
2. **Transient is besmettelijk in kosten:** het oplossen van een transient service bouwt ook elke transient waarvan hij afhangt, hoe diep ook.
3. **Tel constructor-aanroepen** voor alles dat data laadt: een constructor die één keer per request draait voor data die nooit verandert, is precies deze bug.
4. Let op de omgekeerde fout, *captive dependencies*: een singleton die van een scoped service afhangt, houdt daar voor altijd één instantie van vast. `ValidateScopes` vangt dat in development.

## Extra credit
Registreer `PriceCatalog` in plaats daarvan als **scoped**. Hoe vaak wordt hij gebouwd per 1.500 requests, en waarom lost dat het niet op? Maak daarna `PricingService` een singleton en laat `PriceCatalog` transient. Waarom slaagt dat *ook*, en waarom is het een slechtere fix?

## Verder graven
De prijzen veranderen in het echt af en toe wel. Hoe zou je de singleton herladen zonder de app te herstarten, en zonder dat een request ooit een halfgebouwde tabel ziet? (Kijk naar het verwisselen van een referentie met `Volatile.Write`/`Interlocked.Exchange`, of `IOptionsMonitor`.)
