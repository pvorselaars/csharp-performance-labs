using OrderFeed; using PerfLab.Harness; using PerfLab.Harness.Raven;
return Lab.Run(new LabSpec(
    Name: "S-DB01-01-order-feed",
    Workload: Workload.Run,
    ExpectedChecksum: 143750,
    MaxMetrics: new() { [RavenMetrics.RequestsPerOp] = 1, [Metrics.Time] = 10 }), args);
