# L02-05 - Oplossing

## Wat het profiel laat zien

- **allocaties:** ~2.000.000 `System.Threading.Tasks.Task<decimal>`-instanties, één per call. Bij de zeldzame miss verschijnt ook de state-machine-box.
- **tracing:** niets "traags" valt op: alleen `AsyncTaskMethodBuilder`-frames. Het is de dood door kleine allocaties, niet door een CPU-hotspot.

## Grondoorzaak
`async Task<decimal>` retourneert bij **elke** call een `Task<decimal>`-object, zelfs wanneer `TryGetValue` slaagt en de methode nooit await't. Twee miljoen calls is twee miljoen tasks. Er is geen gecachete `Task<decimal>` voor willekeurige decimal-waarden, dus elk resultaat krijgt zijn eigen task.

## Fix
Retourneer `ValueTask<decimal>`. Retourneer op het hot path `new ValueTask<decimal>(cached)`, een struct met de waarde erin en geen heap-allocatie. Alleen het zeldzame miss-pad heeft een echte `Task` nodig, dus verplaats dat naar een aparte `async Task<decimal>`-helper en wrap die. (Een niet-`async`-methode die direct `ValueTask` retourneert vermijdt ook de state machine op het snelle pad.)

## Lessen
1. `async` maakt niets sneller; het maakt *wachten* goedkoop. Als de methode meestal niet wacht, betaal je voor machinerie die je niet gebruikt.
2. **`ValueTask` heeft regels:** await hem één keer, blokkeer er niet op, bewaar hem niet. Converteer bij twijfel met `.AsTask()`. De standaardkeuze voor publieke API's blijft `Task`; gebruik `ValueTask` waar een profiel laat zien dat de allocatie ertoe doet.
3. Alternatieve fix: cache de `Task<decimal>` per key (`Task.FromResult` één keer). Dat alloceert niets per call, en elk aantal callers kan hem awaiten.
4. Op het echte async-pad (`Task.Yield()` hier) kost `ValueTask` ongeveer evenveel als `Task`: zijn winst zit op het synchrone pad.

## Ga verder
Wat als de cache 50% misses had? Meet zowel `Task` als `ValueTask` bij hitrates van 100%, 90% en 50%. Waar verdwijnt het voordeel?