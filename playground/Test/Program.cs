global using Senpai;

namespace Test
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            Benchmark(() => App.Run(new(args)));
        }
    }
}