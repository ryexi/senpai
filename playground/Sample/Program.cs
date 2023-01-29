namespace Sample
{
    internal class Program
    {
        static int Main(string[] args)
        {
            return Cli.Run(new(args, "A simple sample."));
        }
    }
}