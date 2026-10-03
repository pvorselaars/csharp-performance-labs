using PerfLab.Harness;
using PerfLab.Harness.Raven;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;

namespace CountryReport;

public class Customer { public string Id { get; set; } = ""; public string Country { get; set; } = ""; }
public class Order { public string Id { get; set; } = ""; public string CustomerId { get; set; } = ""; public int Cents { get; set; } public string Note { get; set; } = ""; }

public static class Db
{
    static readonly string[] Countries = ["NL", "DE", "FR", "BE", "ES"];

    public static readonly IDocumentStore Store = RavenDb.CreateStore("country-report", store =>
    {
        using var bulk = store.BulkInsert();
        for (var c = 1; c <= 100; c++) bulk.Store(new Customer { Id = "customers/" + c, Country = Countries[c % 5] });
        for (var o = 0; o < 4000; o++) bulk.Store(new Order { Id = "orders/" + o, CustomerId = "customers/" + (o % 100 + 1), Cents = 10 + (o * 29) % 990, Note = new string('n', 300) });
    }, store =>
    {
        using var s = store.OpenSession();
        s.Query<Order>().Take(1).ToList();
    });
}

public static class Workload
{
    /// <summary>Totals the orders placed by Dutch customers.</summary>
    public static long Run()
    {
        RequestCounter.Reset();
        using var session = Db.Store.OpenSession();
        long total = 0;
        var orders = session.Query<Order>().Include(o => o.CustomerId).ToList();
        foreach (var o in orders)
            if (session.Load<Customer>(o.CustomerId).Country == "NL") total += o.Cents;
        Lab.Report(RavenMetrics.RequestsPerOp, RequestCounter.Total);
        return total;
    }
}
