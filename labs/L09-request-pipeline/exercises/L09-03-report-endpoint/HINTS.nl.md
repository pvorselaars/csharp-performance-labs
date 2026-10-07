# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

dotTrace aan serverzijde: tijd in `Response.Body.FlushAsync` en overhead per call bij het schrijven. Op de lijn laten `ss`/`tcpdump` veel kleine segmenten zien; de harness laat de latency zien.
</details>

<details><summary>Hint 2: waar?</summary>

Welke twee calls gebeuren 300 keer per request, en heeft de *client* er hier baat bij dat de eerste byte vroeg aankomt?
</details>

<details><summary>Hint 3: waarom?</summary>

Elke `FlushAsync` dwingt de bytes het socket op (een systeemcall en een netwerksegment) en elke `WriteAsync` heeft vaste overhead. Streamen is terecht voor langlopende of enorme responses; voor een body van 9 KB bouw je hem en schrijf je hem één keer. (Nog beter: schrijf rechtstreeks naar `Response.BodyWriter` zonder een string op te bouwen.)
</details>
