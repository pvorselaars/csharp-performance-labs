using TicketImport; using PerfLab.Harness; using PerfLab.Harness.Raven;
return Lab.Run(new LabSpec(
    Name: "S-DB01-13-ticket-import",
    Workload: Workload.Run,
    ExpectedChecksum: 33,
    MaxMetrics: new() { [RavenMetrics.RequestsPerOp] = 6, [Metrics.Time] = 60 }), args);
