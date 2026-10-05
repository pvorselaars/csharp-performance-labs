# L07-03 - Works on my laptop

*Twenty Cores, One Problem*

## Symptoom
De service zag er prima uit op een laptop van een developer, maar op de buildserver met 20 cores gebruikt dezelfde workload, die bijna geen langlevende data heeft, **meer dan 300 MB committed memory** en ziet de geheugengrafiek van de container er alarmerend uit. De code heeft geen leak: achtergebleven-na-volledige-GC is minimaal.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 300 ref-ms |
| Mediaan toegewezen | 5000 MB |
| committedMB | ≤ 25 |
| workingSetMB | ≤ 150 |

## Boss fight-regels
- **Alleen het symptoom.** Hints zijn met opzet generiek. Schrijf de post-mortem (`templates/POSTMORTEM.md`) *voordat* je de oplossing leest.
