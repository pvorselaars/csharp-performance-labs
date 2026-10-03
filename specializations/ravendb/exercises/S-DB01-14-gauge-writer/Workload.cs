using PerfLab.Harness;
using PerfLab.Harness.Raven;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations.Revisions;
using Raven.Client.ServerWide;

namespace GaugeWriter;

public class Gauge { public string Id { get; set; } = ""; public int Reading { get; set; } }

public static class Db
{
    public static readonly IDocumentStore Store = RavenDb.CreateStore("gauge-writer", store =>
        store.Maintenance.Send(new ConfigureRevisionsOperation(new RevisionsConfiguration
        {
            Default = new RevisionsCollectionConfiguration { Disabled = false }
        })));
}

public static class Workload
{

    /// <summary>Ten live gauges are overwritten with a new reading sixty times.</summary>
    public static long Run()
    {
        RequestCounter.Reset();
        var stamp = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
        long total = 0;
        for (var round = 1; round <= 60; round++)
        {
            using var session = Db.Store.OpenSession();
            for (var g = 1; g <= 10; g++)
                session.Store(new Gauge { Id = "gauges/" + stamp + "-" + g, Reading = round * g });
            session.SaveChanges();
        }
        for (var g = 1; g <= 10; g++) total += 60 * g;

        using var check = Db.Store.OpenSession();
        Lab.Report(RavenMetrics.RevisionsPerDoc, check.Advanced.Revisions.GetCountFor("gauges/" + stamp + "-1"));
        return total;
    }
}
