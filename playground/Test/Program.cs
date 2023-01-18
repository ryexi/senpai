global using Senpai;

using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Test
{
    internal partial class Program
    {
        [ModuleInitializer]
        public static void Benchmark()
        {
            var sw = Stopwatch.StartNew();
            AppDomain.CurrentDomain.ProcessExit += (s, e) 
                => Console.WriteLine("\nTime elapsed: {0} ms", sw?.ElapsedMilliseconds);
        }

        static int Main(string[] args)
        {
            return App.Run(args);
        }
    }
}