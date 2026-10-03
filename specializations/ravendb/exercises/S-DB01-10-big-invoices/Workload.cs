using PerfLab.Harness;
using PerfLab.Harness.Raven;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;

namespace BigInvoices;

public class OrderLine { public int Qty { get; set; } public int Cents { get; set; } }
public class Invoice { public string Id { get; set; } = ""; public int Number { get; set; } public List<OrderLine> Lines { get; set; } = new(); public string Note { get; set; } = ""; }

public static class Db
{
    public static readonly IDocumentStore Store = RavenDb.CreateStore("big-invoices", store =>
    {
        using var bulk = store.BulkInsert();
        for (var i = 1; i <= 3000; i++)
        {
            var inv = new Invoice { Id = "invoices/" + i, Number = i, Note = new string('n', 300) };
            for (var l = 0; l < 10; l++) inv.Lines.Add(new OrderLine { Qty = 1 + (i + l) % 5, Cents = 10 + (i * l) % 90 });
            bulk.Store(inv);
        }
    }, store =>
    {
        using var s = store.OpenSession();
        s.Query<Invoice>().Take(1).ToList();
    });
}

public static class Workload
{
    /// <summary>Finds the big invoices (computed total above 1 740 cents) and folds their numbers into a checksum.</summary>
    public static long Run()
    {
        RequestCounter.Reset();
        using var session = Db.Store.OpenSession();
        long checksum = 0;
        foreach (var inv in session.Query<Invoice>().ToList())
            if (inv.Lines.Sum(l => l.Qty * l.Cents) > 1740) checksum += inv.Number;
        Lab.Report(RavenMetrics.RequestsPerOp, RequestCounter.Total);
        return checksum;
    }
}
