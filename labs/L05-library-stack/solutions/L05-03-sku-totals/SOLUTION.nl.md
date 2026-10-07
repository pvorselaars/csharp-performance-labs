# L05-03 - Oplossing

## Wat het profiel laat zien

- **Sampling:** self-time in de native SQLite-library (een volledige table scan per query); EF en jouw code zijn nauwelijks zichtbaar.
- **Query-plan:** `SCAN Lines` ervoor; `SEARCH Lines USING INDEX IX_Lines_Sku (Sku=?)` erna.

## Grondoorzaak
De `WHERE Sku = @p`-query heeft geen ondersteunende index, dus elke uitvoering is een volledige table scan: O(rijen) per query in plaats van O(log rijen + matches).

## Fix
Voeg een index toe op `Sku` (`modelBuilder.Entity<Line>().HasIndex(l => l.Sku)`), aangemaakt met het schema. In productie zou je hem toevoegen via een migratie, en het plan controleren.

## Lessen
1. **Een trage query met een snel ogende C#-regel is een plan-probleem.** Lees het query-plan.
2. Indexeer de kolommen waarop je filtert, joint en sorteert; maar elke index kost schrijfprestaties en ruimte, dus voeg ze toe voor gemeten queries.
3. Het correleren van profiler-tijd (native DB-frames) met `EXPLAIN` is de vaardigheid: de profiler vertelt je *waar*, het plan vertelt je *waarom*.
4. Datavolume doet ertoe: dit is onzichtbaar bij 1.000 rijen en pijnlijk bij 200.000.

## Ga verder
Voeg een samengestelde index toe voor `WHERE Sku = @p AND Cents > @min`. Hoe verandert het plan, en waarom maakt kolomvolgorde uit?
