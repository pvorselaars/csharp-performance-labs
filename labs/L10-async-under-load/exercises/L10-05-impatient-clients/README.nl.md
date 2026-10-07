# L10-05 - Ongeduldige clients

*Talking to an Empty Room*

## Symptoom
De meeste clients geven hun request na 30 ms op (timeouts, gesloten tabbladen), maar de server **blijft voor elk van hen de volle 200 ms werk doen**. De server kan maar 4 requests tegelijk aan (denk aan een database-pool of een downstream-limiet), dus dat verspilde werk is niet gratis: afgebroken requests nemen slots in, en de clients die *wel* nog op een antwoord wachten staan achter hen in de rij. Tijdens een incident wordt het erger: trage requests worden opnieuw geprobeerd, de afgebroken originelen blijven draaien, de belasting verdubbelt.

De belasting is 100 requests van 20 gebruikers. Elke vierde request komt van een geduldige client die op zijn antwoord wacht; de rest geeft het na 30 ms op. De harness rapporteert hoe lang de geduldige clients wachtten (p99), hoeveel requests er tegelijk in de server zaten (`peakInFlight`), en hoeveel werkstappen er nog liepen nadat de client al weg was (`stepsAfterAbort`).

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd (tot de server weer idle is) | 2000 ms |
| p99-latency van de voltooide requests | 1500 ms |
| Mediaan toegewezen | 8 MB |
| peakInFlight | ≤ 25 |
| stepsAfterAbort | 0 |

> De exercise draait een ASP.NET Core-server op loopback **binnen het harness-proces** en stuurt die aan met virtuele gebruikers (`WebRig`). De tijd wordt gedomineerd door vaste waits, dus de tijdsbudgetten zijn niet machineschaalbaar. Het *minimum* aantal threads van de thread pool staat voorafgaand aan elke run vast op 4 (`Workload.Reset`, hulpconstructie, niet de fix), zodat het effect niet afhangt van je aantal cores. Allocatie en CPU omvatten de kleine constante clientkosten.
