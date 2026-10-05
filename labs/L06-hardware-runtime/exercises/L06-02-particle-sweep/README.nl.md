# L06-02 - Particle sweep

*Particle Physics*

## Symptoom
Het bezoeken van 2 miljoen deeltjes en het optellen van vijf velden duurt **~20 ms**: ongeveer 10 ns per deeltje, veel meer dan vijf loads en een paar vermenigvuldigingen. Er wordt niets gealloceerd in de loop.

## Doel
Hetzelfde resultaat (checksum), en:

| Budget | Waarde |
|---|---|
| Mediane tijd | 5 ref-ms |
| Mediaan gealloceerd | 0 MB |
