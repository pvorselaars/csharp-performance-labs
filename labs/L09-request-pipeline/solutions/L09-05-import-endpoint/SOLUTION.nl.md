# L09-05 - Oplossing

## Wat het profiel laat zien

- **Allocaties:** per request, één `string` van ~340 KB (de hele body in UTF-16, op de LOH), de `StringBuilder`/`char[]`-buffers die `ReadToEndAsync` onderweg laat groeien, en UTF-8-transcodeerbuffers wanneer de string geparst wordt. De `ImportLine`-objecten zelf zijn maar een klein deel.
- **GC:** ~35 gen2-collecties per run: LOH-allocaties tellen mee voor gen2, dus elke request brengt de volgende volledige collectie dichterbij.

## Grondoorzaak
De handler zet de request-body om in een string voordat hij hem parst: een UTF-8-body van 170 KB wordt een UTF-16-string van 340 KB op de Large Object Heap, opgebouwd via tussenliggende buffers, en daarna weer getranscodeerd naar UTF-8 voor de parser, bij elke request.

Het model is al mager: `ImportLine` declareert alleen `Id` en `Qty`, en `System.Text.Json` slaat de omschrijvingen, SKU's en prijzen over zonder er iets voor toe te wijzen. De stringkopie is dus niet een klein deel van de kosten, het is vrijwel alles.

## Fix
Parse rechtstreeks vanuit de request-stream: `await ctx.Request.ReadFromJsonAsync<ImportBatch>()` (of `JsonSerializer.DeserializeAsync<ImportBatch>(ctx.Request.Body)`). De serializer leest de body in gepoolde UTF-8-chunks, en de payload bestaat nooit als één groot object. Het model binden als handler-parameter (`async (ImportBatch batch) => ...`) doet hetzelfde.

## Lessen
1. **Zet request-bodies niet om in strings** tenzij je de tekst echt nodig hebt; stream ze naar de parser.
2. **Een mager model helpt alleen als de parser de stream ziet.** Ongebruikte properties overslaan is gratis, maar niet nadat de hele body eerst in een string is gekopieerd.
3. LOH-grote rommel bij elke request uit zich in gen2-collecties en latency-pieken, niet als één trage functie (Lab 2, weer).
4. Stel sowieso een request-groottelimiet in (`MaxRequestBodySize`): een onbegrensde body is ook een denial-of-service-vector.

## Extra credit
Roep `ctx.Request.EnableBuffering()` aan vóór het parsen in de fix. Wat doet dat met de allocatie, en wanneer zou je het daadwerkelijk nodig hebben (bijvoorbeeld om de body te loggen na een mislukte parse)?

## Verder graven
Maak de batch 10.000 regels en vergelijk het piekgeheugen van beide versies. Tel daarna een enorme batch op zonder elke regel in het geheugen te houden: `JsonSerializer.DeserializeAsyncEnumerable<ImportLine>` streamt een array op *topniveau*, dus dat zou een andere payload-vorm vereisen; met deze vorm kunnen een `PipeReader` (`ctx.Request.BodyReader`) en een `Utf8JsonReader` `Lines` één item tegelijk doorlopen.
