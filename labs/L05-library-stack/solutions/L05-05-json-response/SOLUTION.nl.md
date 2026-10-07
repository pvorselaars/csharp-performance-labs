# L05-05 - Oplossing

## Wat het profiel laat zien
- **Sampling:** het grootste deel van de tijd is metadata-constructie: `JsonSerializerOptions`/`JsonTypeInfo`-creatie, factory-werk van `JsonStringEnumConverter`, property- en naming-policy-setup, herhaald bij alle 100 requests. Het daadwerkelijke schrijven is een klein deel.
- **Allocatie:** een nieuwe set metadata-objecten per request, plus een UTF-16 `string` en een UTF-8 `byte[]` ter grootte van elke response.

## Grondoorzaak
Twee losse misbruiken van de library, elk te fixen op zijn eigen regel:

1. **Options per call.** `JsonSerializerOptions` bevat de metadata-cache. Een verse instance per request bouwt die elke keer opnieuw op. .NET 7+ verzacht dit door de cache te delen tussen *gelijke* options, maar een `new JsonStringEnumConverter()` is elke keer een andere instance, dus zijn geen twee options ooit gelijk en wordt niets hergebruikt.
2. **Via een string gaan.** `Serialize` > UTF-16 `string` > `Encoding.UTF8.GetBytes` > `Write` kopieert de body twee keer voordat hij bij de socket aankomt. De serializer kan UTF-8 direct in de stream schrijven met gepoolde buffers.

## Fix
Declareer de JSON-conventies van de API één keer, in een source-generated `JsonSerializerContext` (`camelCase`, `UseStringEnumConverter`, `WhenWritingNull`), en roep `JsonSerializer.Serialize(responseBody, dto, ApiJson.Default.OrdersPage)` aan. Een `static readonly JsonSerializerOptions` plus `Serialize(stream, ...)` voldoet ook. Source generation verplaatst het metadata-werk bovendien naar compile time en maakt de code trim- en AOT-safe.

In ASP.NET Core krijg je dit via `Results.Json(dto, ApiJson.Default.OrdersPage)` of `ConfigureHttpJsonOptions`: configureer de options één keer bij startup, en het framework schrijft direct naar de response body.

## Lessen
1. **`JsonSerializerOptions` is een cache, geen settings-zakje.** Maak hem één keer aan en hergebruik hem (analyzer CA1869 signaleert het per-call-patroon).
2. "Gelijke options delen een cache" valt uit elkaar zodra een converter inline wordt ge-`new`'t.
3. **Serialiseer één keer, naar de uiteindelijke encoding, direct naar de bestemming.** Ga niet via `string` wanneer het wire-formaat UTF-8 is.
4. Source generation (`[JsonSerializable]`) geeft de conventies één compile-time-gecheckt thuis.

## Extra credit
Controleer de splitsing in "Grondoorzaak" zelf: fix elk probleem apart en meet het. Verwijder daarna de regel `Converters = { ... }`, laat de rest zoals hij was, en meet opnieuw. Waarom maakt die ene regel zoveel uit?

## Ga verder
Gebruik `JsonSerializer.SerializeAsync` tegen een echte `HttpListener`-response en vergelijk. Geef daarna een `IAsyncEnumerable<Order>` terug voor een ongepagineerde export en kijk hoe het geheugen vlak blijft naarmate het aantal rijen groeit.
