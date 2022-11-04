using System.Diagnostics;

namespace Testing
{
    public partial class Program
    {
        public static void Benchmark(Action action)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                action?.Invoke();
            }
            finally
            {
                Console.WriteLine("\nTime elapsed: {0} ms", stopwatch.ElapsedMilliseconds);
            }
        }
    }
}