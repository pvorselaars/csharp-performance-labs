namespace PerfLab.Harness.Raven;

/// <summary>
/// Metric names reported via <c>Lab.Report</c> and gated via <c>LabSpec.MaxMetrics</c> by the RavenDB exercises,
/// so the string used to report a value and the string used to budget it can't drift apart.
/// </summary>
public static class RavenMetrics
{
    /// <summary>Server round trips made by one operation (see <see cref="RequestCounter"/>).</summary>
    public const string RequestsPerOp = "requestsPerOp";

    /// <summary>Server round trips across the whole workload (see <see cref="RequestCounter.Total"/>).</summary>
    public const string TotalRequests = "totalRequests";

    /// <summary>Stored revisions of one document at the end of a run (the revisions exercises).</summary>
    public const string RevisionsPerDoc = "revisionsPerDoc";
}
