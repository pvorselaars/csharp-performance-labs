using NoteSweep; using PerfLab.Harness; using PerfLab.Harness.Raven;
return Lab.Run(new LabSpec(
    Name: "S-DB01-07-note-sweep",
    Workload: Workload.Run,
    ExpectedChecksum: 4003,
    MaxMetrics: new() { [RavenMetrics.RequestsPerOp] = 1, [Metrics.Alloc] = 12, [Metrics.Time] = 150 }), args);
