using PerfLab.Harness;
using PerfLab.Harness.Raven;
using Raven.Client.Documents;

namespace TicketImport;

public class Ticket { public string Id { get; set; } = ""; public string Status { get; set; } = ""; public long Stamp { get; set; } }

public static class Db
{
    public static readonly IDocumentStore Store = RavenDb.CreateStore("ticket-import", _ => { }, store =>
    {
        using var s = store.OpenSession();
        s.Query<Ticket>().Where(t => t.Status == "open").Take(1).ToList();
    });
}

public static class Workload
{
    /// <summary>Imports 100 tickets and reports how many of them are open.</summary>
    public static long Run()
    {
        RequestCounter.Reset();
        var stamp = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
        long open = 0;
        using (var session = Db.Store.OpenSession())
        {
            for (var i = 1; i <= 100; i++)
                session.Store(new Ticket { Id = "tickets/" + i, Status = i % 3 == 0 ? "open" : "closed", Stamp = stamp });
            session.SaveChanges();
        }
        using (var session = Db.Store.OpenSession())
            open = session.Query<Ticket>().Customize(x => x.WaitForNonStaleResults()).Count(t => t.Status == "open");
        Lab.Report(RavenMetrics.RequestsPerOp, RequestCounter.Total);
        return open;
    }
}
