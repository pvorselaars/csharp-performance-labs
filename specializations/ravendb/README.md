# RavenDB specialization (S-DB01)

Prerequisites: Lab 4, Lab 11. Not needed for Labs 0–14.

Each exercise runs an in-process RavenDB server, seeded once outside the timed region. The harness counts client-to-server round trips (`requestsPerOp`), so the budgets are deterministic; time and allocation back them up.

Skills across the track: sessions and change tracking, queries and projections, static and map-reduce indexes, facets, streaming, bulk operations, patching, and revisions.

Run one: `dotnet run -c Release --project specializations/ravendb/exercises/<id>`

| Exercise | Title |
|---|---|
| [S-DB01-01](exercises/S-DB01-01-order-feed/README.md) | Order feed |
| [S-DB01-02](exercises/S-DB01-02-job-sweeper/README.md) | Job sweeper |
| [S-DB01-03](exercises/S-DB01-03-revenue-tile/README.md) | Revenue tile |
| [S-DB01-04](exercises/S-DB01-04-finance-page/README.md) | Finance page |
| [S-DB01-05](exercises/S-DB01-05-article-export/README.md) | Article export |
| [S-DB01-06](exercises/S-DB01-06-sensor-ingest/README.md) | Sensor ingest |
| [S-DB01-07](exercises/S-DB01-07-note-sweep/README.md) | Note sweep |
| [S-DB01-08](exercises/S-DB01-08-red-products/README.md) | Red products |
| [S-DB01-09](exercises/S-DB01-09-leaderboard/README.md) | Leaderboard |
| [S-DB01-10](exercises/S-DB01-10-big-invoices/README.md) | Big invoices |
| [S-DB01-11](exercises/S-DB01-11-country-report/README.md) | Country report |
| [S-DB01-12](exercises/S-DB01-12-brand-sidebar/README.md) | Brand sidebar |
| [S-DB01-13](exercises/S-DB01-13-ticket-import/README.md) | Ticket import |
| [S-DB01-14](exercises/S-DB01-14-gauge-writer/README.md) | Gauge writer |
| [S-DB01-15](exercises/S-DB01-15-contract-audit/README.md) | Contract audit |
| [S-DB01-boss](exercises/S-DB01-boss-sales-dashboard/README.md) | Sales dashboard |

Further reading: the [RavenDB documentation](https://ravendb.net/docs), client API section.
