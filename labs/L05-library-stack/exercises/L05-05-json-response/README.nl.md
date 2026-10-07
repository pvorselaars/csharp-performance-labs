# L05-05 - JSON-response

*Return to Sender*

## Symptoom
Het endpoint `GET /orders?page=N` geeft 50 orders per pagina terug als JSON (~5 KB per response). Het bedienen van alle 100 pagina's duurt **~35 ms en wijst ~8,6 MB toe**, ongeveer **17x de daadwerkelijk verstuurde bytes**. De handler doet niets anders dan een DTO serialiseren, dus de kosten zitten volledig in hoe `System.Text.Json` gebruikt wordt.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 5 ref-ms |
| Mediaan toegewezen | 1 MB |

## Opmerking
`ChecksumStream` staat in voor het netwerk. Hij vingerafdrukt de response-bytes, dus jouw uitvoer moet **byte-identiek** blijven: dezelfde property-namen, dezelfde enum-strings, dezelfde weggelaten nulls.
