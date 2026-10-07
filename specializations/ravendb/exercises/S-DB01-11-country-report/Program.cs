using CountryReport; using PerfLab.Harness; using PerfLab.Harness.Raven;
return Lab.Run(new LabSpec(
    Name: "S-DB01-11-country-report",
    Workload: Workload.Run,
    ExpectedChecksum: 402850,
    MaxMetrics: new() { [RavenMetrics.RequestsPerOp] = 1, [Metrics.Alloc] = 12, [Metrics.Time] = 35 }), args);
