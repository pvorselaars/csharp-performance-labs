# L04-06 - Reference data

*Mostly Harmless*

## Symptoom
Referentiedata wordt door acht workers voortdurend gelezen en slechts een handvol keer bijgewerkt. **Twee miljoen reads duren langer dan op één thread.** Elke read is één dictionary-lookup.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 20 ref-ms |
| Mediaan toegewezen | 2 MB |

## Opmerking
Vereist **minimaal 4 cores** om het effect te laten zien. De exercise start zijn eigen threads/tasks, dus het resultaat hangt niet af van hoe druk jouw machine is, maar een 2-core machine zal de verbetering onderrapporteren.
