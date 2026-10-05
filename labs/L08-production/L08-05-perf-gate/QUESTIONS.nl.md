# Vragen
1. **Wat gate je?** Voor elke metriek: allocatie per run, CPU/wall time, GC-aantallen, retained memory, p99-latency. Zeg of hij *exact* of *ruis* is, en of hij een merge moet blokkeren, moet waarschuwen, of alleen gevolgd moet worden over tijd.
2. **Van SLO naar budget.** De SLO van je service is "p99 < 300 ms bij 200 requests/seconde op 4 cores". Een baseline-run toont p99 = 180 ms en 1,2 MB allocatie per request. Schrijf de budgetten die je zou vastleggen voor: p99, geallokeerde bytes per request, en foutpercentage. Hoeveel speelruimte, en waarom?
3. **Ruis.** Drie manieren waarop een tijdsgebaseerde gate flaky wordt (gedeelde runners, thermische toestand, tiered JIT, andere tenants). Noem voor elke de mitigatie die deze harness al gebruikt (of nodig zou hebben).
4. **Herhalingsbeleid.** Een gate faalt één keer en slaagt bij een herhaling. Wat doe je (stilzwijgend herhalen? twee keer herhalen? N opeenvolgende fails vereisen?), en wat kost dat beleid je?
5. **Gamen.** Hoe zou iemand de gate groen kunnen krijgen zonder iets te verbeteren (bijv. het budget verhogen, de workload verzwakken)? Welke reviewregel voorkomt dat?
6. **Baseline-drift.** Budgetten worden geleidelijk losser en niemand merkt het. Stel een mechanisme voor (bijv. een ratel: budgetten kunnen alleen verkrapt worden zonder een expliciete, gereviewde uitzondering).
