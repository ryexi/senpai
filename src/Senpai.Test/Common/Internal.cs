using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Senpai.Test;

internal static class Internal
{
    [ModuleInitializer]
    public static void Benchmark()
    {
        var sw = Stopwatch.StartNew();
        AppDomain.CurrentDomain.ProcessExit += (s, e)
            => Console.WriteLine("\nTime elapsed: {0} ms", sw?.ElapsedMilliseconds);
    }
}