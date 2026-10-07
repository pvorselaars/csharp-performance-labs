# L07-01 - Checkout service

*Launch Day Jitters*

## Symptoom
Bij de lancering komen 60 checkout-requests tegelijk binnen. Elke request heeft 20 regelitems waarvan de prijs uit een cache komt die wordt gevoed door een database met 3 ms latency. De **traagste checkout duurt ongeveer 0,4 seconde**, de CPU staat grotendeels stil, en de database zelf is ook niet in de buurt van druk bezet. En dat is nog het goede geval: de **allereerste batch na het starten van de service duurt ongeveer 40 seconden**. Verkeer in productie zal veel hoger liggen.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 15 ref-ms |
| Mediaan toegewezen | 2 MB |
| Mediane p99-latency | 15 ref-ms |
| Eerste run | 30 ref-ms |

## Boss fight-regels
- **Alleen het symptoom.** Er zijn meerdere defecten en het oplossen van het ene legt meestal het volgende bloot; meet opnieuw na elke verandering.
- Hints zijn met opzet generiek. Gebruik `templates/POSTMORTEM.md` en schrijf de post-mortem *voordat* je de oplossing leest.
- Er staan budgetten op meerdere metrics tegelijk; aan één voldoen is niet genoeg.
