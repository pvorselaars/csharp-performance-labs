using PerfLab.Harness;
using PerfLab.Harness.Raven;
using Raven.Client.Documents;

namespace JobSweeper;

public class Job { public string Id { get; set; } = ""; public int Points { get; set; } public long Stamp { get; set; } public string Payload { get; set; } = ""; }

public static class Db
{
    public static readonly IDocumentStore Store = RavenDb.CreateStore("job-sweeper", store =>
    {
        using var bulk = store.BulkInsert();
        for (var i = 1; i <= 400; i++) bulk.Store(new Job { Id = "jobs/" + i, Points = i % 7, Payload = new string('x', 400) });
    }, store =>
    {
        using var s = store.OpenSession();
        s.Query<Job>().Take(1).ToList();
    });
}

public static class Workload
{

    /// <summary>Marks every pending job as picked up and returns a checksum of what was processed.</summary>
    public static long Run()
    {
        RequestCounter.Reset();
        using var session = Db.Store.OpenSession();
        var jobs = session.Query<Job>().Take(400).ToList();
        long total = 0;
        foreach (var j in jobs)
        {
            j.Stamp = DateTime.UtcNow.Ticks;
            session.SaveChanges();
            total += j.Points + 1;
        }
        Lab.Report(RavenMetrics.RequestsPerOp, RequestCounter.Total);
        return total;
    }
}
