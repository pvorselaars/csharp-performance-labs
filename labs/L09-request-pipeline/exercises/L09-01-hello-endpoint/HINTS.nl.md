# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

Meet bytes en CPU **per request** (het totaal van de harness ÷ 4.000). Profileer daarna de allocaties: welke types worden per request toegewezen, en door welke laag (Kestrel, routing, MVC, formatters)?
</details>

<details><summary>Hint 2: waar?</summary>

Volg één request door de pipeline: routing -> controller-activatie -> action-invocatie -> result-executie -> output-formatter. Welke van die lagen bestaan er voor een minimale API?
</details>

<details><summary>Hint 3: waarom?</summary>

MVC geeft je model binding, filters, content negotiation en formatters, en die kosten allocaties en CPU per request, zelfs voor een triviale action. Een minimaal endpoint dat tekst of een kant-en-klaar result teruggeeft, slaat ze over. Dat is geen argument tegen MVC; het vertelt je de *bodem* waartegen je je echte endpoints kunt afzetten.
</details>
