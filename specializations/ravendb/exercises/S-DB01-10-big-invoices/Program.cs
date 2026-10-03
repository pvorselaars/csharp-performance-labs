using BigInvoices; using PerfLab.Harness; using PerfLab.Harness.Raven;
return Lab.Run(new LabSpec(
    Name: "S-DB01-10-big-invoices",
    Workload: Workload.Run,
    ExpectedChecksum: 451671,
    MaxMetrics: new() { [RavenMetrics.RequestsPerOp] = 1, [Metrics.Alloc] = 12, [Metrics.Time] = 40 }), args);
