using PerfLab.Harness;
using PerfLab.Harness.Raven;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;

namespace BrandSidebar;

public class Product { public string Id { get; set; } = ""; public string Brand { get; set; } = ""; public string Category { get; set; } = ""; public string Description { get; set; } = ""; }

public class Products_Facets : AbstractIndexCreationTask<Product>
{
    public Products_Facets()
    {
        Map = products => from p in products select new { p.Brand, p.Category };
    }
}

public static class Db
{
    public static readonly string[] Brands = ["acme", "bolt", "crest", "dune", "echo", "flux", "gale", "halo"];

    public static readonly IDocumentStore Store = RavenDb.CreateStore("brand-sidebar", store =>
    {
        using var bulk = store.BulkInsert();
        for (var i = 1; i <= 6000; i++)
            bulk.Store(new Product { Id = "products/" + i, Brand = Brands[(i * 7) % 8], Category = "c" + i % 5, Description = new string('d', 500) });
    }, store =>
    {
        new Products_Facets().Execute(store);
        using var s = store.OpenSession();
        s.Query<Product, Products_Facets>().Where(p => p.Category == "c3").AggregateBy(f => f.ByField(p => p.Brand)).Execute();
    });
}

public static class Workload
{
    /// <summary>The category page sidebar: how many products each brand has in category c3.</summary>
    public static long Run()
    {
        RequestCounter.Reset();
        using var session = Db.Store.OpenSession();
        long checksum = 0;
        var facets = session.Query<Product, Products_Facets>().Where(p => p.Category == "c3").AggregateBy(f => f.ByField(p => p.Brand)).Execute();
        foreach (var v in facets["Brand"].Values) checksum += (Array.IndexOf(Db.Brands, v.Range) + 1) * v.Count;
        Lab.Report(RavenMetrics.RequestsPerOp, RequestCounter.Total);
        return checksum;
    }
}
