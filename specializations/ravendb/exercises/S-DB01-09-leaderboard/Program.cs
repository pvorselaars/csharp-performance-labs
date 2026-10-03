using Leaderboard; using PerfLab.Harness; using PerfLab.Harness.Raven;
return Lab.Run(new LabSpec(
    Name: "S-DB01-09-leaderboard",
    Workload: Workload.Run,
    ExpectedChecksum: 998995,
    MaxMetrics: new() { [RavenMetrics.RequestsPerOp] = 1, [Metrics.Alloc] = 2, [Metrics.Time] = 10 }), args);
