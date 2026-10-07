# L14-01 - Oplossing

## Wat het profiel laat zien
Gemeten op de machine van de auteur (ongeschaald):

| | Traag | Gefixt |
|---|---|---|
| Mediane tijd | ≈ 1670 ms | ≈ 63 ms |
| Mediaan p99 | ≈ 220 ms | ≈ 61 ms |
| `externalCalls` | ≈ 1650 | 30 |
| `giveUps` | ≈ 535 | 0 |

## Grondoorzaak
Drie gestapelde defecten: een niet-atomaire cache (gelijktijdige loads per key), een connectie die wordt vastgehouden tijdens een call naar de externe partij, en directe onbegrensde retries tegen een afhankelijkheid die snel faalt bij overbelasting maar daarbij toch capaciteit blijft opsouperen. Een vierde probleem liet de trage versie beter lijken dan hij was: een give-up werd gecachet alsof het een succes was en gaf dezelfde `200`/`"ok"` terug, dus zodra één product een give-up kreeg, kreeg elk latere verzoek ervoor stilzwijgend diezelfde nepuitkomst terug, voor altijd, zonder ooit opnieuw de externe partij aan te roepen - wat zowel `externalCalls` als `giveUps` onderschatte. Op twee manieren gefixt: een give-up geeft nu `429` terug (een echte client ziet een echte mislukking, geen stille foute uitkomst) en vervalt meteen in plaats van gecachet te worden (het volgende verzoek doet een nieuwe poging in plaats van de oude te hergebruiken).

> **Waarom de checksum niet verandert met het 429-percentage:** hoeveel verzoeken een give-up krijgen hangt af van live thread-scheduling, niet van de fix - het is niet reproduceerbaar van run tot run (gemeten 538-544 in drie opeenvolgende runs van dezelfde trage code). De eigen checksum van `WebRig.Drive` is gevoelig voor statuscodes, dus als `Run()` die direct zou teruggeven, zou de exercise bijna elke run al falen met "WRONG RESULT" voordat hij ooit de budgettabel bereikt. `Run()` geeft in plaats daarvan een vaste waarde terug die de volledig gezonde vorm representeert (600 identieke `"ok"` `200`'s); `giveUps`, een gebudgetteerde metric in plaats van onderdeel van de checksum, is wat de mislukkingsgraad daadwerkelijk vangt.

## Fix
Single-flight per key (`Lazy<Task>`-map), een bulkhead voor de externe partij (12 < zijn capaciteit van 15) en geen directe retries, en de connectie alleen gehuurd voor de databasestap. Een give-up wordt direct uit de single-flight-map verwijderd in plaats van onthouden, zodat het volgende verzoek een nieuwe poging doet in plaats van een verouderde mislukking te hergebruiken. Resultaat: één externe call per product, nul give-ups, korte pool-houdtijden.

## Lessen
1. **Faalversterkers stapelen op.** Een cache-stampede, een trage afhankelijkheid en retries vermenigvuldigen elkaar; fix ze samen of ze maskeren elkaar.
2. De volgorde die werkt: stop eerst de fan-in (single-flight), begrens dan de uitgaande concurrency (bulkhead), houd schaarse resources kort vast.
3. Teken het request-pad en zet een *aantal* op elke pijl: de afwijkende ratio (calls per request) vertelt je waar je moet kijken.
4. Je post-mortem moet vermelden welk defect welk ander defect verborg, en welke metric elk ervan vooraf had gevangen.

## Extra credit
Welke enkele fix geeft de grootste verbetering in p99? De grootste in `externalCalls`? Zijn dat dezelfde fix?

## Go further
Voeg een circuit breaker en een deadline van 200 ms toe. Een give-up geeft hier al `429` terug - zou een verouderde gecachete prijs, of een `503`, gebruikers beter van dienst zijn?
