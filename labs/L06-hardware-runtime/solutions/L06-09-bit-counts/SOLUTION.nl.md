# L06-09 - Oplossing

## Wat het profiel laat zien
- **Sampling:** de binnenste `while`-loop; data-afhankelijk aantal iteraties (~32) met misvoorspelde exits.

## Grondoorzaak
Een handgeschreven bit-tel-loop, terwijl er één hardware-instructie voor bestaat.

## Fix
`BitOperations.PopCount(w)`: intrinsic, branchless, constante tijd.

## Lessen
1. **Kijk in `System.Numerics.BitOperations`, `Math`, `MemoryExtensions` en `Vector` voordat je trucs schrijft.**
2. Hardware-intrinsics zijn op een portable manier beschikbaar: de JIT kiest de instructie, met een fallback.
3. Data-afhankelijke loops misvoorspellen; constante-tijd-operaties niet (L06-03).

## Extra credit
Draai met `DOTNET_EnableHWIntrinsic=0`. Wat kost `PopCount` als het hardwarepad is uitgeschakeld?

## Ga verder
Tel de popcounts over de array op met `Vector128`/`Popcnt.X64`-intrinsics of `TensorPrimitives.PopCount`. Is dat nog sneller?
