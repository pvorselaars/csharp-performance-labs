using GaugeWriter; using PerfLab.Harness; using PerfLab.Harness.Raven;
return Lab.Run(new LabSpec(
    Name: "S-DB01-14-gauge-writer",
    Workload: Workload.Run,
    ExpectedChecksum: 3300,
    MaxMetrics: new() { [RavenMetrics.RevisionsPerDoc] = 5 }), args);
