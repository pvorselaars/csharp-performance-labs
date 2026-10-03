using PerfLab.Harness;
using PerfLab.Harness.Raven;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.Client.Documents.Linq;

namespace SalesDashboard;

public class Customer { public string Id { get; set; } = ""; public string Name { get; set; } = ""; public string Region { get; set; } = ""; }
public class Line { public string Sku { get; set; } = ""; public string Note { get; set; } = ""; public int Cents { get; set; } }
public class Order { public string Id { get; set; } = ""; public string CustomerId { get; set; } = ""; public int Total { get; set; } public List<Line> Lines { get; set; } = new(); }

public static class Db
{
    public static readonly IDocumentStore Store = RavenDb.CreateStore("sales-dashboard", store =>
    {
        using var bulk = store.BulkInsert();
        for (var c = 1; c <= 100; c++) bulk.Store(new Customer { Id = "customers/" + c, Name = "Customer " + c, Region = c % 4 == 0 ? "north" : "other" });
        for (var o = 0; o < 4000; o++)
        {
            var order = new Order { Id = "orders/" + o, CustomerId = "customers/" + (o % 100 + 1), Total = 10 + (o * 29) % 990 };
            for (var l = 0; l < 20; l++) order.Lines.Add(new Line { Sku = "SKU-" + l, Note = new string('n', 60), Cents = l });
            bulk.Store(order);
        }
    }, store =>
    {
        new Orders_ByCustomer().Execute(store);
        using var s = store.OpenSession();
        s.Query<Customer>().Where(c => c.Region == "north").Take(1).ToList();
        s.Query<Orders_ByCustomer.Result, Orders_ByCustomer>().Take(1).ToList();
    });
}

public class Orders_ByCustomer : AbstractIndexCreationTask<Order, Orders_ByCustomer.Result>
{
    public class Result { public string CustomerId { get; set; } = ""; public long Total { get; set; } }

    public Orders_ByCustomer()
    {
        Map = orders => from o in orders select new Result { CustomerId = o.CustomerId, Total = o.Total };
        Reduce = results => from r in results group r by r.CustomerId into g select new Result { CustomerId = g.Key, Total = g.Sum(x => x.Total) };
    }
}

public static class Workload
{
    /// <summary>The "north region" dashboard: every north customer with the total of all their orders.</summary>
    public static long Run()
    {
        RequestCounter.Reset();
        using var session = Db.Store.OpenSession();
        long checksum = 0;
        var customers = session.Query<Customer>().Where(c => c.Region == "north").ToList();
        var ids = customers.Select(c => c.Id).ToList();
        var totals = session.Query<Orders_ByCustomer.Result, Orders_ByCustomer>().Where(r => r.CustomerId.In(ids)).ToList();
        foreach (var r in totals) checksum += int.Parse(r.CustomerId["customers/".Length..]) * r.Total;
        Lab.Report(RavenMetrics.RequestsPerOp, RequestCounter.Total);
        return checksum;
    }
}
