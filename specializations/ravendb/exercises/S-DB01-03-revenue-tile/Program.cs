using RevenueTile; using PerfLab.Harness; using PerfLab.Harness.Raven;
return Lab.Run(new LabSpec(
    Name: "S-DB01-03-revenue-tile",
    Workload: Workload.Run,
    ExpectedChecksum: 824850,
    MaxMetrics: new() { [Metrics.Alloc] = 14, [Metrics.Time] = 40 }), args);
