# Lab 4: async & concurrency

Code die op zichzelf snel is en in de praktijk toch traag: threads die op elkaar wachten, locks die een tel te lang vastgehouden worden, werk dat sneller opstapelt dan het wegstroomt.

**Vaardigheden:** Thread-pool starvation, contention, back-pressure, waarom sampling misleidt als threads zitten te wachten.

**Meesterschapstoets:** Diagnosticeer een "traag maar idle CPU"-service aan de hand van de timeline.

Draai er een: `dotnet run -c Release --project labs/L04-async-concurrency/exercises/<id>` (verwacht `FAIL`), werk hem uit zoals beschreven in de [hoofd-README](../../README.md), en open pas daarna de oplossing.

**Verder lezen:** [Lab 4-leeslijst](../../docs/READING-LIST.md#lab-4-async-concurrency)

## Exercises
| Exercise | Scenario | Spoiler |
|---|---|---|
| [L04-01-request-burst](exercises/L04-01-request-burst/README.md) | Request burst | [oplossing](solutions/L04-01-request-burst/SOLUTION.md) |
| [L04-02-latency-stats](exercises/L04-02-latency-stats/README.md) | Latency stats | [oplossing](solutions/L04-02-latency-stats/SOLUTION.md) |
| [L04-03-bulk-fetch](exercises/L04-03-bulk-fetch/README.md) | Bulk fetch | [oplossing](solutions/L04-03-bulk-fetch/SOLUTION.md) |
| [L04-04-config-cache](exercises/L04-04-config-cache/README.md) | Config cache | [oplossing](solutions/L04-04-config-cache/SOLUTION.md) |
| [L04-05-order-pipeline](exercises/L04-05-order-pipeline/README.md) | Order pipeline | [oplossing](solutions/L04-05-order-pipeline/SOLUTION.md) |
| [L04-06-reference-data](exercises/L04-06-reference-data/README.md) | Reference data | [oplossing](solutions/L04-06-reference-data/SOLUTION.md) |
| [L04-07-partner-api-calls](exercises/L04-07-partner-api-calls/README.md) | Partner API calls | [oplossing](solutions/L04-07-partner-api-calls/SOLUTION.md) |

## Eindbaas-gevecht
Een vermomde combinatie van de defecten uit dit lab: geen hints per defect. Doe hem als laatste, en schrijf daarna op welk defect uit welke exercise kwam.

| Exercise | Scenario | Spoiler |
|---|---|---|
| [L04-boss-notification-hub](exercises/L04-boss-notification-hub/README.md) | Notification hub | [oplossing](solutions/L04-boss-notification-hub/SOLUTION.md) |
