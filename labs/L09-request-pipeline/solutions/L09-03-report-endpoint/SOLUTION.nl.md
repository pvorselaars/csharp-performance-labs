# L09-03 - Oplossing

## Wat het profiel laat zien
- **Sampling:** tijd in `FlushAsync`, `WriteAsync`, Kestrels output-pipe-writer/socket-send-pad; allocatie van de geïnterpoleerde strings per regel.

## Grondoorzaak
Flushen na elke regel maakt van één kleine response honderden socket-writes en continuations; de overhead per response wordt vermenigvuldigd met het aantal regels.

## Fix
Bouw de body één keer op (`StringBuilder`, of beter, schrijf rechtstreeks naar `BodyWriter`) en schrijf hem in één call; laat Kestrel aan het eind van de response flushen. Houd streamen (met bewuste flushes) aan voor echt langlevende of grote responses.

## Lessen
1. **Flush niet per item**, tenzij een client er baat bij heeft items vroeg te ontvangen.
2. Praatzucht is een latency-vermenigvuldiger: vaste kosten per call x aantal calls.
3. Geef de voorkeur aan `BodyWriter`/`IBufferWriter<byte>` en `Utf8`-formattering voor endpoints met hoge doorvoer; vermijd `string`-tussenstappen.
4. `Response.WriteAsync(string)` codeert bij elke call naar UTF-8; één grote write codeert maar één keer.

## Extra credit
Houd de writes per regel, maar verwijder de `FlushAsync`. Hoeveel van de verbetering krijg je zo, en waar komt de rest vandaan?

## Verder graven
Schrijf met `BodyWriter` (`Encoding.UTF8.GetBytes(line, writer.GetSpan(...))`) zonder `string`-allocaties. Vergelijk de allocaties.
