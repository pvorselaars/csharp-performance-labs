using PerfLab.Harness;
using PerfLab.Harness.Raven;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations.Revisions;
using Raven.Client.ServerWide;

namespace ContractAudit;

public class Contract { public string Id { get; set; } = ""; public int Value { get; set; } }

public static class Db
{
    public static DateTime AsOf;

    public static readonly IDocumentStore Store = RavenDb.CreateStore("contract-audit", store =>
    {
        store.Maintenance.Send(new ConfigureRevisionsOperation(new RevisionsConfiguration
        {
            Default = new RevisionsCollectionConfiguration { Disabled = false }
        }));
        using (var s = store.OpenSession())
        {
            for (var i = 1; i <= 100; i++) s.Store(new Contract { Id = "contracts/" + i, Value = i });
            s.SaveChanges();
        }
        Thread.Sleep(200);
        AsOf = DateTime.UtcNow;
        Thread.Sleep(200);
        using (var s = store.OpenSession())
        {
            for (var i = 1; i <= 100; i++) s.Store(new Contract { Id = "contracts/" + i, Value = i * 1000 });
            s.SaveChanges();
        }
    });
}

public static class Workload
{
    /// <summary>The audit: what was the total value of the 100 contracts at the audit date, before they were revised?</summary>
    public static long Run()
    {
        RequestCounter.Reset();
        using var session = Db.Store.OpenSession();
        var ids = Enumerable.Range(1, 100).Select(i => "contracts/" + i).ToList();
        long total = 0;
        foreach (var id in ids) total += session.Advanced.Revisions.Get<Contract>(id, Db.AsOf).Value;
        Lab.Report(RavenMetrics.RequestsPerOp, RequestCounter.Total);
        return total;
    }
}
