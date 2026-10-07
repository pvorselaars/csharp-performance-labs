# Post-mortem

Schrijf dit na elk eindbaas-gevecht en elke capstone, *voordat* je een oplossing leest. Een blaamvrij format: beschrijf het systeem, niet de persoon - niemand leert iets van een verslag dat stiekem gaat over hoe dom iemand zich voelde.
Houd het onder één pagina. De waarde zit in "waarom duurde het zo lang om te vinden?".

```
## <incident naam>, <datum>

#### Samenvatting
Eén of twee zinnen: wat gebruikers zagen, hoe lang, hoe erg.

#### Impact
Cijfers: p50/p99 voor en na, foutpercentage, piekgeheugen, RPS op dat moment.

#### Tijdlijn
Waar ik naar keek, in volgorde, en wat elke stap me vertelde (inclusief de verkeerde afslagen).

#### Grondoorzaken
Elk afzonderlijk defect, in de volgorde waarin ze aan het licht kwamen. Noteer welke welke maskeerde.

##### Detectie
Hoe had ik dit in productie kunnen opmerken? Welke metric, alert of budget had dit opgevangen?

##### Fix
Wat ik veranderde, één regel per defect, en de meting die elk bevestigde.

#### Wat ik fout had
Hypotheses die ik had en die onjuist bleken, en welk bewijs ze ontkrachtte.

#### Preventie
Eén regressie-gate (budget, test, alert) per grondoorzaak.

#### Eerdere exercise die hierop lijkt
bijv. L02-03 LOH-churn
```

Verder lezen: de post-mortem-hoofdstukken van Google's *Site Reliability Engineering*-boek ([[73]](../docs/READING-LIST.md#ref73)).
