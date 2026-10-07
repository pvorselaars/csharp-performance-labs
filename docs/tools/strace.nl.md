# strace
> Alleen Linux

**Beantwoordt:** hoe vaak roept het programma het besturingssysteem aan, en waarvoor? Bestanden openen, lezen, schrijven, sockets, slapen. Gebruik het als een profiler tijd toont "in de kernel" of "buiten jouw code" en je wilt weten wat die tijd was.

## Installeren
```bash
sudo dnf install strace                                # Fedora
sudo apt install strace                                # Ubuntu/Debian
```

## Commando's
```bash
# samenvattingstabel: aantal en tijd per system call (-f volgt elke thread)
strace -f -c dotnet labs/<lab>/exercises/<id>/bin/Release/net10.0/<id>.dll

# alleen bestandsgerelateerde calls
strace -f -c -e trace=%file,read,write,close dotnet <...>.dll

# koppel een tijdje aan een draaiend proces, Ctrl+C om te stoppen en de samenvatting af te drukken
strace -f -c -p <pid>
```
`-f` is belangrijk: .NET is multithreaded, en zonder die vlag zie je alleen de hoofdthread.

## Hoe je het leest
```
% time     seconds  usecs/call     calls    errors syscall
------ ----------- ----------- --------- --------- ----------------
 61.20    0.412345           1    400123           read
```
- Kijk eerst naar **calls**. Vergelijk het aantal met hoeveel werk het programma doet. 400.000 `read`-calls om een bestand van 400 KB te lezen is er één per byte.
- `openat`/`close`-tellingen die overeenkomen met het aantal verwerkte items betekenen dat er per item een bestand geopend wordt.
- `futex` is threads die op elkaar wachten. Dat zit er altijd in bij .NET. Het telt alleen mee als het domineert.

## Valkuilen
- **strace vertraagt elke system call flink.** Vertrouw de *aantallen*, niet de tijden.
- Het telt de hele run mee, opstart inbegrepen. Draai het op een normale (niet-profile) run, en vergelijk de exercise met de oplossing.
- Koppelen aan een proces dat je niet zelf gestart hebt, kan `sudo` vereisen, afhankelijk van `kernel.yama.ptrace_scope`.

## Documentatie
[strace man page](https://man7.org/linux/man-pages/man1/strace.1.html)
