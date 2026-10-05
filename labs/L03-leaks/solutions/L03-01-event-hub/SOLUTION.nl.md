# L03-01 - Oplossing

## Wat het profiel laat zien
- **snapshots:** 2.000 extra `Widget` en 2.000 extra `byte[]` (10.000 bytes). Retentiepad: `Hub` (static field) → event-delegate (`MulticastDelegate`-invocation list) → `Widget`.
- **tracing:** publicatietijd groeit met elke run: meer subscribers om aan te roepen; elke `+=` kopieert ook de hele invocation list.

## Grondoorzaak
De hub leeft gedurende het hele proces; elke `Widget` abonneert zich met `hub.Published += OnMessage`, wat een delegate opslaat die naar de widget wijst in de lijst van de hub. Niets verwijdert hem ooit, dus de hub (een GC-root via een static field) houdt elke widget en zijn 10 KB-buffer in leven. Dit is het klassieke **event-handler-lek**; het is ook een langzaam lek in CPU, want elke publicatie roept nu elke dode widget aan.

## Fix
Maak `Widget` `IDisposable`, meld je af in `Dispose` (`-=`), en gebruik `using`. Een veiligere algemene vorm: een weak-event-patroon, of laat de hub een subscription-token teruggeven dat zich afmeldt zodra het gedisposed wordt.

## Take-aways
1. **Events zijn references.** De publisher houdt elke subscriber vast zolang de publisher leeft.
2. Symptomen: geheugen dat alleen maar stijgt in stap met *object-creatie*, plus operaties die na verloop van tijd trager worden.
3. Lambdas maken het erger: `hub.Published += x => ...this...` kan niet afgemeld worden zonder de delegate in een variabele te bewaren.
4. De behouden-na-volledige-GC-gate is de lektest: een gezonde workload laat vrijwel niets achter.

## Extra credit
Voeg `GC.Collect()` toe aan het eind van `Run` in een scratch-kopie. Verandert het behouden geheugen? Waarom is dat het verkeerde instinct voor een lek?

## Go further
Implementeer de subscription-token-versie (`IDisposable Subscribe(Action<int>)`). Schrijf daarna de weak-event-versie met `WeakReference<T>` en noem de nadelen (wie ruimt de dode entries op?).
