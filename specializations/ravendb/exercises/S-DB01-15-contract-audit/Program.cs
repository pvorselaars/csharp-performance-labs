using ContractAudit; using PerfLab.Harness; using PerfLab.Harness.Raven;
return Lab.Run(new LabSpec(
    Name: "S-DB01-15-contract-audit",
    Workload: Workload.Run,
    ExpectedChecksum: 5050,
    MaxMetrics: new() { [RavenMetrics.RequestsPerOp] = 2 }), args);
