# L04-boss - Oplossing

## Wat het profiel laat zien
- Drie onafhankelijke defecten in drie stages.

## Grondoorzaak
Eén defect uit elk van drie eerdere labs, in drie aparte stages van één batch.

## Fix
Inboxen uitschrijven (disposen); het pure werk buiten de lock uitvoeren en een atomaire add gebruiken; de queue begrenzen.

## Lessen
1. **Defect -> bron:** inboxen die zich nooit uitschrijven bij een langlevende bus = **L03-01**; duur werk binnen een `lock` = **L04-02**; onbegrensde producer/consumer-queue = **L04-05**.
2. Eindbazen testen *herkenning*: welke signatuur (bereikbaar geheugen, contention/idle threads, queue-diepte) wees naar welke stage?

## Go further
Rangschik de drie op hoeveel elk bijdraagt aan het tijdbudget, en daarna aan het geheugen. Zijn dat verschillende volgordes?
