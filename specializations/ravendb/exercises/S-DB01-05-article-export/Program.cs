using ArticleExport; using PerfLab.Harness; using PerfLab.Harness.Raven;
return Lab.Run(new LabSpec(
    Name: "S-DB01-05-article-export",
    Workload: Workload.Run,
    ExpectedChecksum: 1203000,
    MaxMetrics: new() { [RavenMetrics.RequestsPerOp] = 2 }), args);
