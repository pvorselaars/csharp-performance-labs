using PerfLab.Harness;
using PerfLab.Harness.Raven;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;

namespace CountryReport;

public class Customer { public string Id { get; set; } = ""; public string Country { get; set; } = ""; }
public class Order { public string Id { get; set; } = ""; public string CustomerId { get; set; } = ""; public int Cents { get; set; } public string Note { get; set; } = ""; }

public class Orders_ByCountry : AbstractIndexCreationTask<Order, Orders_ByCountry.Result>
{
    public class Result { public string Country { get; set; } = ""; }

    public Orders_ByCountry()
    {
        Map = orders => from o in orders select new Result { Country = LoadDocument<Customer>(o.CustomerId).Country };
    }
}

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
        new Orders_ByCountry().Execute(store);
        using var s = store.OpenSession();
        s.Query<Orders_ByCountry.Result, Orders_ByCountry>().Take(1).ToList();
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
        foreach (var o in session.Query<Orders_ByCountry.Result, Orders_ByCountry>().Where(r => r.Country == "NL").OfType<Order>().ToList()) total += o.Cents;
        Lab.Report(RavenMetrics.RequestsPerOp, RequestCounter.Total);
        return total;
    }
}
