using FinancePage; using PerfLab.Harness; using PerfLab.Harness.Raven;
return Lab.Run(new LabSpec(
    Name: "S-DB01-04-finance-page",
    Workload: Workload.Run,
    ExpectedChecksum: 152010000,
    MaxMetrics: new() { [RavenMetrics.RequestsPerOp] = 2, [Metrics.Alloc] = 2, [Metrics.Time] = 10 }), args);
