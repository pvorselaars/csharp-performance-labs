using PerfLab.Harness;
using PerfLab.Harness.Raven;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;

namespace FinancePage;

public class Entry { public string Id { get; set; } = ""; public string CustomerId { get; set; } = ""; public int Cents { get; set; } }

public static class Db
{
    public static readonly IDocumentStore Store = RavenDb.CreateStore("finance-page", store =>
    {
        using var bulk = store.BulkInsert();
        for (var e = 0; e < 12000; e++) bulk.Store(new Entry { Id = "entries/" + e, CustomerId = "customers/" + (e % 100 + 1), Cents = 1 + (e * 37) % 500 });
    }, store =>
    {
        using var s = store.OpenSession();
        s.Query<Entry>().Take(1).ToList();
    });
}

public static class Workload
{
    /// <summary>Totals the ledger per customer and folds the result into a checksum.</summary>
    public static long Run()
    {
        RequestCounter.Reset();
        using var session = Db.Store.OpenSession();
        long checksum = 0;
        var totals = new Dictionary<string, long>();
        using var stream = session.Advanced.Stream(session.Query<Entry>());
        while (stream.MoveNext())
            totals[stream.Current.Document.CustomerId] = totals.GetValueOrDefault(stream.Current.Document.CustomerId) + stream.Current.Document.Cents;
        foreach (var (id, cents) in totals) checksum += int.Parse(id["customers/".Length..]) * cents;
        Lab.Report(RavenMetrics.RequestsPerOp, RequestCounter.Total);
        return checksum;
    }
}
