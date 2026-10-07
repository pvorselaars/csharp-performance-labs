# L12-05 - Oplossing

## Grondoorzaak
Onmiddellijke, onbegrensde retries tegen een overbelaste dependency versterken de load die de fouten veroorzaakte (positieve feedback). De extra calls zijn niet gratis: een "snel falen"-afwijzing bezet nog steeds een van de beperkte workers van de downstream voor de volledige duur, dus een vloedgolf aan retries concurreert met echte requests om dezelfde capaciteit. Meer load erin betekent dat iedereen - niet alleen de geretryde requests - langer wacht.

## Fix
Begrens de concurrency richting de dependency (`SemaphoreSlim(15)`, onder de capaciteit van 20 van de downstream), zodat deze nooit overbelast raakt; retries zijn dan zelden nodig. Voeg in productie timeouts toe, begrensde retries met jittered exponential backoff, en een circuit breaker (`Microsoft.Extensions.Http.Resilience`).

## Lessen
1. **Retries zijn load.** Zonder grenzen veranderen ze een hikje in een storing.
2. Verkies preventie (concurrency limits, load shedding) boven reactie (retries).
3. Als je retryt: begrens het aantal pogingen, voeg exponential backoff met jitter toe, en gebruik een retry-*budget* (bijv. ≤ 10% extra calls).
4. Maak geretryde operaties idempotent, en geef deadlines door.

## Extra credit
Zet de gate op 25 (boven de 20 van de downstream). Wat gebeurt er?

## Ga verder
Voeg jittered backoff toe aan de *trage* versie. Hoe ver kom je daarmee zonder bulkhead?
