# L11-02 - Klantprofiel

*Twenty Times Twenty*

## Symptoom
Het laden van één klant met zijn 20 orders en 20 adressen levert **400 rijen** uit de database op (20 x 20) in plaats van ~40, en de allocatie en latency per request liggen veel hoger dan de data (40 kleine entiteiten) rechtvaardigt. De query ziet eruit als het schoolvoorbeeld. De harness rapporteert het ruwe rijenaantal als `rowsPerRequest`; EF vouwt de gedupliceerde kolommen hoe dan ook terug tot 20 losse `Order`s en 20 losse `Address`es, dus de *gematerialiseerde* objectgraaf lijkt even groot en laat dit niet zien - alleen het rijenaantal doet dat.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 50 ref-ms |
| Mediaan toegewezen | 35 MB |
| Mediane p99-latency | 5 ref-ms |
| rowsPerRequest | 41 |
| commandsPerRequest | < 4 |

> De exercise draait een ASP.NET Core-server op loopback **binnen het harness-proces** (`WebRig`) en stuurt die aan met virtuele gebruikers. Databases zijn in-memory SQLite, één keer geseed. Allocatie en CPU bevatten de kleine constante client-kosten.
