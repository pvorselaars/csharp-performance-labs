using PerfLab.Harness;
using PerfLab.Harness.Raven;
using Raven.Client.Documents;

namespace OrderFeed;

public class Customer { public string Id { get; set; } = ""; public string Name { get; set; } = ""; public int Tier { get; set; } }
public class Order { public string Id { get; set; } = ""; public string CustomerId { get; set; } = ""; public int Cents { get; set; } }

public static class Db
{
    public static readonly IDocumentStore Store = RavenDb.CreateStore("order-feed", store =>
    {
        using var bulk = store.BulkInsert();
        for (var c = 1; c <= 200; c++) bulk.Store(new Customer { Id = "customers/" + c, Name = "Customer " + c, Tier = c % 5 });
        for (var o = 1; o <= 100; o++) bulk.Store(new Order { Id = "orders/" + o, CustomerId = "customers/" + (o * 2), Cents = 100 + (o * 13) % 900 });
    });
}

public static class Workload
{
    /// <summary>Builds the "latest 100 orders" feed: each order's cents weighted by its customer's tier.</summary>
    public static long Run()
    {
        RequestCounter.Reset();
        using var session = Db.Store.OpenSession();
        long total = 0;
        var orders = session.Query<Order>().Include(o => o.CustomerId).Take(100).ToList();
        foreach (var o in orders)
        {
            var customer = session.Load<Customer>(o.CustomerId);
            total += o.Cents * (customer.Tier + 1);
        }
        Lab.Report(RavenMetrics.RequestsPerOp, RequestCounter.Total);
        return total;
    }
}
