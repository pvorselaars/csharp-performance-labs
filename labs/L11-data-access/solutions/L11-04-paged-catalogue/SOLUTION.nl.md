# L11-04 - Oplossing

## Wat het profiel laat zien

- **Bewaard geheugen:** duizenden `Product` + snapshots + strings, geworteld in de change tracker van de statische context.

## Grondoorzaak
Een singleton/statische trackende `DbContext` stapelt elke geladen entiteit op (en heeft een lock nodig omdat hij niet thread-safe is).

## Fix
Eén kortlevende context per request/unit of work (`AddDbContext` scoped, of pooled met `AddDbContextPool`) en `AsNoTracking` voor read-only queries.

## Lessen
1. **`DbContext` = unit of work, kortlevend, niet thread-safe.** Registreer hem nooit als singleton.
2. De change tracker is een cache waar je niet om vroeg: daarom groeien langlevende contexts.
3. Locks rond een gedeelde context verbergen een ontwerpfout en serialiseren je server.
4. Dit is L3 (retentie) binnen een ASP.NET-app: dezelfde tools (snapshot, dominators) vinden het.

## Extra credit
Houd de gedeelde context maar roep `ChangeTracker.Clear()` aan na elke request. Lost dat de retentie op? Wat klopt er nog steeds niet?

## Verder gaan
Registreer `AddDbContextPool` en vergelijk de allocatie per request met `new`-contexts.
