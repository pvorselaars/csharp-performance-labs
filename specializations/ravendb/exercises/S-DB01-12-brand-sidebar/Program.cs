using BrandSidebar; using PerfLab.Harness; using PerfLab.Harness.Raven;
return Lab.Run(new LabSpec(
    Name: "S-DB01-12-brand-sidebar",
    Workload: Workload.Run,
    ExpectedChecksum: 5400,
    MaxMetrics: new() { [RavenMetrics.RequestsPerOp] = 1, [Metrics.Alloc] = 1, [Metrics.Time] = 6 }), args);
