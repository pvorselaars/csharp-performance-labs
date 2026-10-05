# L13-01 - Oplossing

## Grondoorzaak
Een `Connection: close`-header schakelt keep-alive uit: één TCP-connectie per request.

## Fix
Verwijder de header. Verifieer in echte deployments keep-alive van begin tot eind: client pool, load balancer idle-timeout versus Kestrel's `KeepAliveTimeout`, en HTTP/2 waar beschikbaar.

## Lessen
1. **Connection-opzet kan domineren bij kleine requests.** Let op connecties per request, niet alleen latency.
2. Config gekopieerd uit snippets brengt bugs mee: review middleware- en proxyconfiguratie als code.
3. Timeouts moeten correct genest zijn: client < LB-idle-timeout < server keep-alive (anders krijg je resets op hergebruikte connecties).

## Extra credit
Zet `KeepAliveTimeout` in plaats daarvan op 1 ms. Wat gebeurt er met `connections`?

## Go further
Schakel HTTP/2 in Kestrel in en vergelijk connecties en latency bij 16 gelijktijdige gebruikers.
