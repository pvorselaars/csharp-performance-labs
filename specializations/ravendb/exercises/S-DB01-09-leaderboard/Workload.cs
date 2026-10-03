using PerfLab.Harness;
using PerfLab.Harness.Raven;
using Raven.Client.Documents;

namespace Leaderboard;

public class Player { public string Id { get; set; } = ""; public string Name { get; set; } = ""; public int Points { get; set; } public string Bio { get; set; } = ""; }

public static class Db
{
    public static readonly IDocumentStore Store = RavenDb.CreateStore("leaderboard", store =>
    {
        using var bulk = store.BulkInsert();
        for (var i = 1; i <= 5000; i++) bulk.Store(new Player { Id = "players/" + i, Name = "Player " + i, Points = (i * 7919) % 100003, Bio = new string('b', 500) });
    }, store =>
    {
        using var s = store.OpenSession();
        s.Query<Player>().OrderByDescending(p => p.Points).Take(1).ToList();
    });
}

public static class Workload
{
    /// <summary>The leaderboard: sum of the points of the ten best players.</summary>
    public static long Run()
    {
        RequestCounter.Reset();
        using var session = Db.Store.OpenSession();
        long total = 0;
        foreach (var p in session.Query<Player>().ToList().OrderByDescending(p => p.Points).Take(10)) total += p.Points;
        Lab.Report(RavenMetrics.RequestsPerOp, RequestCounter.Total);
        return total;
    }
}
