# L04-01 - Request burst

*Idle Hands*

## Symptoom
Een burst van 200 requests, elk met een database-lookup van 20 ms, duurt in totaal **honderden milliseconden** (de traagste request wacht ~0,6 s), terwijl de CPU de hele tijd vrijwel idle blijft. Eén enkele request duurt op zichzelf 20 ms. Er wordt niets berekend; requests *wachten op elkaar*.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 25 ref-ms |
| Mediaan toegewezen | 1 MB |
| Mediane p99-latency | 25 ref-ms |

## Opmerking
De harness zet het *minimum* aantal threads van de thread pool voor elke run vast op 4 (`Workload.Reset`), zodat het effect niet afhangt van hoeveel cores je hebt. Dat legt alleen het startpunt vast; de pool kan nog steeds groeien. Dat is scaffolding, **niet** de fix.
De grootte van het effect varieert met runtime-versie en machine; op de machine waarop dit geschreven is, was het ruwweg 10-25x.
