# dotnet-stack

**Beantwoordt:** wat doet elke managed thread *op dit moment*? Het drukt één snapshot van alle stacks af en stopt. Het is de snelste manier om te zien *waarop* een vastgelopen of trage proces wacht.

## Commando's
```bash
dotnet-stack report -n <id>
dotnet-stack report -n <id> > stacks-1.txt             # bewaar hem om met een tweede te vergelijken
```

## Hoe je het leest
```
Thread (0x620E9):
  System.Private.CoreLib!System.Buffer.MemmoveInternal(...)
  L1-01-invoice-export!InvoiceExport.InvoiceExporter.Export(...)
  L1-01-invoice-export!InvoiceExport.Workload.Run()
  PerfLab.Harness!PerfLab.Harness.Lab.RunProfile(...)
```
- De **bovenste** regel is waar de thread nu is. Lees omlaag voor hoe hij daar kwam.
- **Zoek naar herhaling.** Eindigen de meeste threads in hetzelfde frame (`Monitor.Enter`, `Task.Wait`, `TaskAwaiter.GetResult`, `Thread.Sleep`, een socket-read), dan is dat het knelpunt.
- Een thread waarvan het bovenste frame een wait is (`WaitHandle.WaitOne`, `Monitor.Wait`) is **inactief**, niet bezig. De runtime heeft er altijd een paar van.

## Valkuilen
- **Het is één moment.** Neem twee of drie reports een paar seconden na elkaar. Een frame dat steeds terugkomt is een patroon; een frame dat je één keer zag kan toeval zijn.
- Het toont alleen managed frames (native code verschijnt als `[Native Frames]`). Voor een tijdlijn van wat threads over de tijd deden, gebruik [dotnet-trace](dotnet-trace.md). Voor stacks samengevoegd op call path, neem een [dump](dotnet-dump.md) en draai `pstacks`.

## Documentatie
[dotnet-stack (Microsoft Learn)](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/dotnet-stack)
