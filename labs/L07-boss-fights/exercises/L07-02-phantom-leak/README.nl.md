# L07-02 - Phantom leak

*The Heap Is Innocent*

## Symptoom
Een service die afbeeldingen verwerkt via een native library laat een **platte managed heap** zien (geen groei in `gc-heap-size`), de leak-gate in deze harness meldt **niets achtergebleven**, en toch loopt het geheugengebruik van het proces op met ongeveer **150 MB per batch** totdat de container wordt gekilld. Heap-snapshots laten niets bijzonders zien: er is aan managed kant niets te vinden.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 6 ref-ms |
| Mediaan toegewezen | 2 MB |
| privateMB | ≤ 1 |
| Achtergebleven na een volledige GC | ≤ 1 MB |

## Boss fight-regels
- **Alleen het symptoom.** Er kunnen meerdere defecten zijn en het oplossen van het ene kan het volgende blootleggen; meet opnieuw na elke verandering.
- Hints zijn met opzet generiek. Gebruik `templates/POSTMORTEM.md` en schrijf de post-mortem *voordat* je de oplossing leest.
