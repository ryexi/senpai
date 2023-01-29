using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Maid.Test;

internal static class Internal
{
    [ModuleInitializer]
    public static void Benchmark()
    {
        var sw = Stopwatch.StartNew();
        AppDomain.CurrentDomain.ProcessExit += (s, e)
            => Console.WriteLine("\nTime elapsed: {0} ms", sw?.ElapsedMilliseconds);
    }

    public static int InvokeProcess(string args, bool debug = false)
    {
        if (string.IsNullOrWhiteSpace(args))
            throw new ArgumentNullException(nameof(args));

        using var process = new Process();

        process.StartInfo = new()
        {
            FileName = "dotnet",
            Arguments = $"{Assembly.GetExecutingAssembly().Location} {args}",
            UseShellExecute = debug,
            CreateNoWindow = !debug
        };

        process.Start();
        process.WaitForExit();

        return process.ExitCode;
    }
}