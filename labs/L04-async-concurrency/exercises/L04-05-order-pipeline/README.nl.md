# L04-05 - Order pipeline

*The Queue That Ate Memory*

## Symptoom
Een snelle producer voedt via een queue een tragere consumer. Alles klopt en de totale tijd is prima, maar **de queue groeit naar duizenden items** (hier 30 MB aan buffers van 10 KB) voordat de consumer bijtrekt. In productie stopt de input nooit, en diezelfde vorm eindigt in een OutOfMemory of een container-kill.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 106 ref-ms |
| Mediaan toegewezen | 73 MB |
| maxQueued | ≤ 76 |

## Opmerking
De harness rapporteert `maxQueued`: het grootste aantal items dat tijdens de run in de queue wachtte. Elk gequeued item is een buffer van 10 KB, dus het is ook een proxy voor het piekgeheugen.
