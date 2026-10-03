using SensorIngest; using PerfLab.Harness; using PerfLab.Harness.Raven;
return Lab.Run(new LabSpec(
    Name: "S-DB01-06-sensor-ingest",
    Workload: Workload.Run,
    ExpectedChecksum: 50500,
    MaxMetrics: new() { [RavenMetrics.RequestsPerOp] = 5, [Metrics.Time] = 120 }), args);
