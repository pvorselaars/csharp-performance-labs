using PerfLab.Harness;
using PerfLab.Harness.Raven;
using Raven.Client.Documents;

namespace ArticleExport;

public class Article { public string Id { get; set; } = ""; public int Views { get; set; } }

public static class Db
{
    public static readonly IDocumentStore Store = RavenDb.CreateStore("article-export", store =>
    {
        using var bulk = store.BulkInsert();
        for (var i = 1; i <= 6000; i++) bulk.Store(new Article { Id = "articles/" + i, Views = 1 + (i * 17) % 400 });
    }, store =>
    {
        using var s = store.OpenSession();
        s.Query<Article>().OrderBy(a => a.Id).Take(1).ToList();
    });
}

public static class Workload
{
    /// <summary>Exports the view count of every article.</summary>
    public static long Run()
    {
        RequestCounter.Reset();
        long total = 0;
        using var session = Db.Store.OpenSession();
        using var stream = session.Advanced.Stream(session.Query<Article>());
        while (stream.MoveNext()) total += stream.Current.Document.Views;
        Lab.Report(RavenMetrics.RequestsPerOp, RequestCounter.Total);
        return total;
    }
}
