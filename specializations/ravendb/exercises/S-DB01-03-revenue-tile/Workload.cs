using PerfLab.Harness;
using PerfLab.Harness.Raven;
using Raven.Client.Documents;

namespace RevenueTile;

public class Line { public string Sku { get; set; } = ""; public string Note { get; set; } = ""; public int Cents { get; set; } }
public class Invoice { public string Id { get; set; } = ""; public int Total { get; set; } public List<Line> Lines { get; set; } = new(); }
public class InvoiceTotal { public int Total { get; set; } }

public static class Db
{
    public static readonly IDocumentStore Store = RavenDb.CreateStore("revenue-tile", store =>
    {
        using var bulk = store.BulkInsert();
        for (var i = 1; i <= 1500; i++)
        {
            var inv = new Invoice { Id = "invoices/" + i, Total = 100 + (i * 31) % 900 };
            for (var l = 0; l < 30; l++) inv.Lines.Add(new Line { Sku = "SKU-" + l, Note = new string('n', 80), Cents = l });
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
    /// <summary>Sums the total of every invoice for the revenue tile.</summary>
    public static long Run()
    {
        RequestCounter.Reset();
        using var session = Db.Store.OpenSession();
        long total = 0;
        foreach (var inv in session.Query<Invoice>().ToList()) total += inv.Total;
        Lab.Report(RavenMetrics.RequestsPerOp, RequestCounter.Total);
        return total;
    }
}
