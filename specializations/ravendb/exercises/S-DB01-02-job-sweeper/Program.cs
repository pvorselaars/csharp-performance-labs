using JobSweeper; using PerfLab.Harness; using PerfLab.Harness.Raven;
return Lab.Run(new LabSpec(
    Name: "S-DB01-02-job-sweeper",
    Workload: Workload.Run,
    ExpectedChecksum: 1598,
    MaxMetrics: new() { [RavenMetrics.RequestsPerOp] = 3, [Metrics.Alloc] = 12, [Metrics.Time] = 100 }), args);
