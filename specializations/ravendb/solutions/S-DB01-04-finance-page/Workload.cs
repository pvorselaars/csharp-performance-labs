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
        new Entries_ByCustomer().Execute(store);
        using var s = store.OpenSession();
        s.Query<Entries_ByCustomer.Result, Entries_ByCustomer>().Take(1).ToList();
    });
}

public class Entries_ByCustomer : AbstractIndexCreationTask<Entry, Entries_ByCustomer.Result>
{
    public class Result { public string CustomerId { get; set; } = ""; public long Cents { get; set; } }

    public Entries_ByCustomer()
    {
        Map = entries => from e in entries select new Result { CustomerId = e.CustomerId, Cents = e.Cents };
        Reduce = results => from r in results group r by r.CustomerId into g select new Result { CustomerId = g.Key, Cents = g.Sum(x => x.Cents) };
    }
}

public static class Workload
{
    /// <summary>Totals the ledger per customer and folds the result into a checksum.</summary>
    public static long Run()
    {
        RequestCounter.Reset();
        using var session = Db.Store.OpenSession();
        long checksum = 0;
        foreach (var r in session.Query<Entries_ByCustomer.Result, Entries_ByCustomer>().ToList())
            checksum += int.Parse(r.CustomerId["customers/".Length..]) * r.Cents;
        Lab.Report(RavenMetrics.RequestsPerOp, RequestCounter.Total);
        return checksum;
    }
}
