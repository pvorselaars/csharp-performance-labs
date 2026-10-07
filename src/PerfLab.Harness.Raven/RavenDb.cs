using Raven.Client.Documents;
using Raven.Client.Documents.Operations.Indexes;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using Raven.Embedded;

namespace PerfLab.Harness.Raven;

/// <summary>
/// Hosts one in-process RavenDB server for an exercise and hands out a seeded database. Call
/// <see cref="CreateStore"/> from a static initialiser so server boot and seeding stay outside the timed region
/// (the same trick the EF exercises use for their SQLite <c>Db</c> class).
/// </summary>
public static class RavenDb
{
    private static readonly object Gate = new();
    private static bool _started;

    /// <summary>
    /// Starts the embedded server on first use, creates a uniquely named database, runs <paramref name="seed"/>
    /// against it, then <paramref name="warmUp"/> (run the exercise's queries once so RavenDB creates any auto-index
    /// they need, and create static indexes), waits for all indexes to catch up, and returns the store with
    /// <see cref="RequestCounter"/> attached and zeroed.
    /// </summary>
    public static IDocumentStore CreateStore(string name, Action<IDocumentStore> seed, Action<IDocumentStore>? warmUp = null)
    {
        lock (Gate)
        {
            if (!_started)
            {
                var dataDir = Path.Combine(Path.GetTempPath(), "perflab-ravendb", Environment.ProcessId.ToString());
                EmbeddedServer.Instance.StartServer(new ServerOptions
                {
                    DataDirectory = dataDir,
                    ServerUrl = "http://127.0.0.1:0",
                    CommandLineArgs = ["--Setup.Mode=None", "--License.Eula.Accepted=true"],
                });
                AppDomain.CurrentDomain.ProcessExit += (_, _) =>
                {
                    EmbeddedServer.Instance.Dispose();
                    try { Directory.Delete(dataDir, recursive: true); } catch (IOException) { }
                };
                _started = true;
            }
        }

        // Build the store by hand rather than EmbeddedServer.GetDocumentStore: that returns an already-initialised
        // store, and OnBeforeRequest can only be subscribed before Initialize().
        var database = name + "-" + Guid.NewGuid().ToString("N")[..8];
        var store = new DocumentStore { Urls = [EmbeddedServer.Instance.GetServerUriAsync().GetAwaiter().GetResult().ToString()], Database = database };
        // Lift Raven's own 30-requests-per-session guard: the exercises must be able to *measure* a chatty session
        // (RequestCounter), not have the client throw before the harness can report it.
        store.Conventions.MaxNumberOfRequestsPerSession = int.MaxValue;
        RequestCounter.Attach(store);
        store.Initialize();
        store.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(database)));
        seed(store);
        warmUp?.Invoke(store);
        WaitForIndexing(store);
        return store;
    }

    /// <summary>
    /// Blocks until every index on <paramref name="store"/> has caught up, then zeroes <see cref="RequestCounter"/>.
    /// Call it at the end of a static initialiser, after one warm-up query has created any auto-index the
    /// exercise needs, so no measured run ever sees stale results or pays for indexing.
    /// </summary>
    public static void WaitForIndexing(IDocumentStore store)
    {
        var deadline = DateTime.UtcNow.AddSeconds(60);
        while (store.Maintenance.Send(new GetIndexesStatisticsOperation()).Any(i => i.IsStale))
        {
            if (DateTime.UtcNow > deadline) throw new TimeoutException("RavenDB indexes did not become non-stale in 60 s.");
            Thread.Sleep(20);
        }
        RequestCounter.Reset();
    }
}
