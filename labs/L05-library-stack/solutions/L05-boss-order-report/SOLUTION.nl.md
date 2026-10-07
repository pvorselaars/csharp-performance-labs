# L05-boss - Oplossing

## Wat het profiel laat zien
- Vier onafhankelijke Lab 5-defecten: N+1-queries, getrackte en over-fetchte entities (een notes-kolom van 800 tekens die niemand leest), geïnterpoleerde logberichten voor een uitgeschakeld niveau, en een open/write/close per bestandsregel.

## Grondoorzaak
Eén defect uit elk van vier Lab 5-exercises.

## Fix
Eén projectie met een server-side aggregaat (`AsNoTracking`, alleen de benodigde kolommen); geen log-formattering per order; één gebufferde writer voor het audit-bestand.

## Lessen
1. **Defect -> bron:** query per klant = **L05-01**; tracking en het laden van de notes-kolom = **L05-02**; `LogDebug($"...")` op een uitgeschakeld niveau = **L05-06**; `File.AppendAllText` per regel = **L05-07**.
2. De **SQL-view** en de **allocatie-view** vinden elk een andere subset: heb je ze allebei gebruikt?
3. De checksum omvat de lengte van het audit-bestand: wat zegt dat je over de inhoud van het bestand?

## Extra credit
Welke fix verwijdert de meeste allocatie? De meeste tijd?

## Ga verder
Schrijf het rapport naar een `Channel`-gevoede achtergrond-writer en bespreek durability.
