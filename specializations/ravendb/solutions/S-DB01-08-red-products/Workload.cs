using PerfLab.Harness;
using PerfLab.Harness.Raven;
using Raven.Client.Documents;

namespace RedProducts;

public class Product { public string Id { get; set; } = ""; public string Tags { get; set; } = ""; public int Price { get; set; } public string Description { get; set; } = ""; }

public static class Db
{
    static readonly string[] Colors = ["red", "blue", "green", "black", "white"];

    public static readonly IDocumentStore Store = RavenDb.CreateStore("red-products", store =>
    {
        using var bulk = store.BulkInsert();
        for (var i = 1; i <= 5000; i++)
            bulk.Store(new Product { Id = "products/" + i, Tags = Colors[i % 5] + " " + Colors[(i * 3 + 1) % 5], Price = 1 + (i * 13) % 200, Description = new string('d', 500) });
    }, store =>
    {
        using var s = store.OpenSession();
        s.Query<Product>().Search(p => p.Tags, "red").Take(1).ToList();
    });
}

public static class Workload
{
    /// <summary>Totals the price of every product tagged "red".</summary>
    public static long Run()
    {
        RequestCounter.Reset();
        using var session = Db.Store.OpenSession();
        long total = 0;
        foreach (var p in session.Query<Product>().Search(p => p.Tags, "red").ToList()) total += p.Price;
        Lab.Report(RavenMetrics.RequestsPerOp, RequestCounter.Total);
        return total;
    }
}
