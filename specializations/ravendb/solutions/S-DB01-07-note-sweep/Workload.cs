using PerfLab.Harness;
using PerfLab.Harness.Raven;
using Raven.Client.Documents;
using Raven.Client.Documents.Commands.Batches;
using Raven.Client.Documents.Operations;

namespace NoteSweep;

public class Note { public string Id { get; set; } = ""; public int Points { get; set; } public long Stamp { get; set; } public string Body { get; set; } = ""; }

public static class Db
{
    public static readonly IDocumentStore Store = RavenDb.CreateStore("note-sweep", store =>
    {
        using var bulk = store.BulkInsert();
        for (var i = 1; i <= 1000; i++) bulk.Store(new Note { Id = "notes/" + i, Points = i % 7, Body = new string('b', 4000) });
    });
}

public static class Workload
{

    /// <summary>Marks every note as reviewed in the nightly sweep.</summary>
    public static long Run()
    {
        RequestCounter.Reset();
        var stamp = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
        using var session = Db.Store.OpenSession();
        long total = 0;
        for (var i = 1; i <= 1000; i++)
        {
            session.Advanced.Defer(new PatchCommandData("notes/" + i, null,
                new PatchRequest { Script = "this.Stamp = args.stamp;", Values = new() { ["stamp"] = stamp } }, null));
            total += i % 7 + 1;
        }
        session.SaveChanges();
        Lab.Report(RavenMetrics.RequestsPerOp, RequestCounter.Total);

        using var check = Db.Store.OpenSession();
        return check.Load<Note>("notes/500").Stamp == stamp ? total : -1;
    }
}
