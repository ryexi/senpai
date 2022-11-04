namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Benchmark(() => App.Run(new(args)));
        }
    }
}