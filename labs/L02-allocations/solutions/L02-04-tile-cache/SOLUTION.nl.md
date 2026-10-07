# L02-04 - Oplossing

## Wat het profiel laat zien

- **Harness:** gen1 = 12 per run in de exercise, 0 na de fix. Allocatie-MB is **identiek** voor en na.
- **timeline:** de finalizer-thread is druk, en de GC-tijd is hoog. In sampling-modus vallen frames binnen het trage pad van de allocator (registratie van finalizable objecten) op.

## Grondoorzaak
`Tile` heeft een finalizer `~Tile()`, gekopieerd uit een "disposable pattern"-snippet, maar bezit geen unmanaged resource: alleen een `byte[]`, die de GC zelf opruimt. Een finalizable object wordt bij allocatie geregistreerd (trager pad); zodra hij rommel wordt, komt hij op de **f-reachable queue** en *overleeft* hij de collectie die hem dood vond, wordt hij gepromoveerd naar de volgende generatie, en wordt hij later gefinaliseerd door de finalizer-thread. Elke tile kost daardoor een extra generatie overleven en een tochtje langs de finalizer-thread.

## Fix
Verwijder de finalizer. Een finalizer is alleen bedoeld voor het direct bezitten van een unmanaged resource, en zelfs dan is een `SafeHandle` het juiste gereedschap. (De `Live`-diagnostische counter ging daarmee mee; heb je er een nodig, verlaag die dan in `Dispose()`.)

## Lessen
1. **Zelfde bytes, 3,5x de tijd.** Allocatie-MB ving dit niet op; het tijdsbudget wel. Niet elke regressie is zichtbaar in één metriek, houd er dus meerdere bij.
2. `SuppressFinalize` herstelt veel (het object komt niet meer in de queue), maar de registratiekosten bij allocatie blijven bestaan, dus het komt niet volledig overeen met "geen finalizer".
3. Vuistregel: **als de klasse alleen managed geheugen bevat, heeft hij geen finalizer en ook niet het volledige dispose-patroon nodig.** Implementeer `IDisposable` wanneer je iets bezit dat disposable is.
4. Finalizers draaien ook op een onvoorspelbaar moment, op één thread, zonder ordeningsgaranties. Ze zijn een laatste redmiddel voor opruimen, geen feature.

## Ga verder
Schrijf de `IDisposable` + finalizer-versie die *correct* is voor een echte native `IntPtr`-handle, vervang hem daarna door een `SafeHandle`, en vergelijk de codegrootte en veiligheid.