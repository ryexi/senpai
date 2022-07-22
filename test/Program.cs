global using Senpai.Token;
using System.Diagnostics;

class Program
{
    private static Stopwatch _stopwatch;

    static Program()
    {
        _stopwatch = new Stopwatch();
        _stopwatch.Start();
    }

    static void Stop()
    {
        _stopwatch.Stop();
        Console.WriteLine("\nTime elapsed: {0} ms", _stopwatch.ElapsedMilliseconds);
    }

    static void Main(string[] args)
    {
        Senpai.Cli.Initialize(args);
        Stop();
    }
}