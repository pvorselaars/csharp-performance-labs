using SalesDashboard; using PerfLab.Harness; using PerfLab.Harness.Raven;
return Lab.Run(new LabSpec(
    Name: "S-DB01-boss-sales-dashboard",
    Workload: Workload.Run,
    ExpectedChecksum: 26160600,
    MaxMetrics: new() { [RavenMetrics.RequestsPerOp] = 3, [Metrics.Alloc] = 2, [Metrics.Time] = 10 }), args);
