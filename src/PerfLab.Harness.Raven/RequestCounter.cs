using Raven.Client.Documents;

namespace PerfLab.Harness.Raven;

/// <summary>
/// Counts HTTP round trips from the RavenDB client to the server, for the RavenDB exercises. Attach once per store
/// via <see cref="Attach"/>. Like <c>CommandCounter.Total</c> in the EF harness: call <see cref="Reset"/> once before
/// a batch, read <see cref="Total"/> once after it.
/// </summary>
public static class RequestCounter
{
    private static int _total;

    /// <summary>The running total since the last <see cref="Reset"/>, across every session on the store.</summary>
    public static int Total => Volatile.Read(ref _total);

    public static void Reset() => Volatile.Write(ref _total, 0);

    /// <summary>Hooks <c>OnBeforeRequest</c> so every request the client sends is counted.</summary>
    public static void Attach(IDocumentStore store) =>
        store.OnBeforeRequest += (_, _) => Interlocked.Increment(ref _total);
}
