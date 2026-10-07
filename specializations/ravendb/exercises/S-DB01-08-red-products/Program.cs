using RedProducts; using PerfLab.Harness; using PerfLab.Harness.Raven;
return Lab.Run(new LabSpec(
    Name: "S-DB01-08-red-products",
    Workload: Workload.Run,
    ExpectedChecksum: 201000,
    MaxMetrics: new() { [RavenMetrics.RequestsPerOp] = 1, [Metrics.Alloc] = 25, [Metrics.Time] = 45 }), args);
