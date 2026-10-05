# Hints (open er één per keer)

<details><summary>Hint 1: welke tool?</summary>

Begin met een sampling-profile, daarna dotMemory-allocaties per type. Zoek naar tijd binnen `System.Text.Json.Serialization.Metadata` (`JsonTypeInfo`, converter-factories, property-setup), niet in het daadwerkelijke schrijven. In het geheugen, zoek naar `string` en `byte[]` ter grootte van elke response.
</details>

<details><summary>Hint 2: waar?</summary>

De serializer bouwt metadata voor elk type dat hij tegenkomt (properties, namen, converters) en cachet dat op de `JsonSerializerOptions`-instance. Hoe lang leeft die options-instance? Tel daarna hoeveel kopieën van elke response bestaan voordat hij bij `responseBody` aankomt.
</details>

<details><summary>Hint 3: waarom?</summary>

Een nieuwe `JsonSerializerOptions` per call gooit de metadata-cache weg. .NET 7+ laat een nieuw options-object de cache van een *gelijk* object hergebruiken, maar converters worden per referentie vergeleken, dus `new JsonStringEnumConverter()` maakt de options van elke call verschillend. Maak de options één keer aan (een `static readonly` field, of een source-generated `JsonSerializerContext`). Los daarvan maakt `Serialize` → `string` (UTF-16) → `GetBytes` → `Write` twee volledige kopieën van de body: `JsonSerializer.Serialize(Stream, ...)` schrijft UTF-8 direct in de stream.
</details>
