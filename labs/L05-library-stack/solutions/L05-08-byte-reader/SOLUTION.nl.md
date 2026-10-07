# L05-08 - Oplossing

## Wat het profiel laat zien
- **`strace -c`:** ~400.000 `read`-syscalls.
- **Sampling**: `RandomAccess.ReadAtOffset`/`read` domineren.

## Grondoorzaak
Buffering uitgeschakeld: één systeemaanroep per byte.

## Fix
Gebruik de standaard gebufferde `FileStream` (of `BufferedStream`, of lees blokken in een `byte[]`/`Span<byte>` of `File.ReadAllBytes`). Hetzelfde resultaat, ~100 systeemaanroepen.

## Lessen
1. **Systeemaanroepen, niet bytes, zijn duur.** Bundel I/O (zie ook L05-07).
2. Standaardinstellingen zijn meestal juist; iemand heeft `bufferSize: 0` ooit om een reden gezet die misschien niet meer van toepassing is: vraag waarom.
3. Voor sequentiële verwerking: lees blokken en verwerk spans; `ReadByte` in een loop is sowieso een geur.

## Extra credit
Probeer `FileOptions.SequentialScan` en `RandomAccess.Read` met een gepoolde buffer.

## Ga verder
Lees in blokken van 64 KB in een `byte[]` en tel op met een span-loop. Vectoriseer daarna de som met `Vector<T>`.
