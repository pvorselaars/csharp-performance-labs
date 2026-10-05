# L09-02 - Oplossing

## Wat het profiel laat zien
- **Allocaties:** `Regex`-internals (parser, code), `Match`/`Group`, geïnterpoleerde `string`, header-strings, per request.

## Grondoorzaak
Lus-invariant werk dat per request gedaan wordt (een `new Regex` in de pipeline) plus gretig geformatteerde logberichten voor een uitgeschakeld niveau.

## Fix
Een `static readonly Regex` (compiled of `[GeneratedRegex]`) en een source-generated logmethode via `[LoggerMessage]`. Middleware draait op **elke** request, dus de kosten per request worden vermenigvuldigd met je hele verkeer.

## Lessen
1. **Middleware zit op het kritieke pad van elke request.** Een microseconde daar is een CPU-seconde per miljoen requests.
2. Alles wat je leerde over allocatie (Labs 1, 2, 5) geldt onveranderd; het requestaantal is wat het vermenigvuldigt.
3. Til alles wat niet van de request afhangt op naar een static of singleton.
4. Meet per request, voor en na; budgetten op bytes/request vangen deze klasse van regressie op.

## Extra credit
Verplaats de logcall naar *na* `next()` (om de statuscode mee te nemen). Wat doet dat met de p99 voor trage requests, en waarom?

## Verder graven
Gebruik `[GeneratedRegex]` in plaats van `RegexOptions.Compiled` en vergelijk. Vervang de regex daarna helemaal door `PathString`/span-parsing.
