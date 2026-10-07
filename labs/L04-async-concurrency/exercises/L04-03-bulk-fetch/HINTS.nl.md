# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

Hier is geen CPU-verhaal te vinden; kijk naar *concurrency*. Wat is het piekaantal in-flight calls, en hoe verandert de latency per call daarmee mee? (De harness rapporteert `peakInflight`.)
</details>

<details><summary>Hint 2: waar?</summary>

`Task.WhenAll(items.Select(...))` start elke call voordat er ook maar één klaar is. Waar zou je een *limiet* neerzetten?
</details>

<details><summary>Hint 3: waarom?</summary>

Concurrency heeft een sweet spot: meer parallellisme verhoogt de doorvoer totdat de downstream verzadigd raakt, waarna latency (en fouten) sneller stijgen dan de doorvoer. Welke API draait een async body over een sequence met een begrensde mate van parallellisme?
</details>
