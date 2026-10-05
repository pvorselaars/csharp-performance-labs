# L05-06 - Debug-logging

*Talking to Nobody*

## Symptoom
De app logt op Debug-niveau in een hot loop, maar productie registreert alleen Warning en hoger, dus die logregels worden **nooit geschreven**. Toch kosten 300.000 ervan **~17 ms en ~41 MB aan allocatie**. De logger staat uit; de kosten niet.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 18 ref-ms |
| Mediaan toegewezen | 1 MB |
