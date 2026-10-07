using PerfLab.Harness;
using PerfLab.Harness.Raven;
using Raven.Client.Documents;

namespace SensorIngest;

public class Reading { public string Id { get; set; } = ""; public string Sensor { get; set; } = ""; public int Value { get; set; } }

public static class Db
{
    public static readonly IDocumentStore Store = RavenDb.CreateStore("sensor-ingest", _ => { });
}

public static class Workload
{
    /// <summary>Ingests a batch of 1 000 sensor readings and returns their sum.</summary>
    public static long Run()
    {
        RequestCounter.Reset();
        long total = 0;
        using var bulk = Db.Store.BulkInsert();
        for (var i = 1; i <= 1000; i++)
        {
            var r = new Reading { Id = "readings/" + i, Sensor = "s" + i % 10, Value = 1 + (i * 7) % 100 };
            bulk.Store(r);
            total += r.Value;
        }
        Lab.Report(RavenMetrics.RequestsPerOp, RequestCounter.Total);
        return total;
    }
}
