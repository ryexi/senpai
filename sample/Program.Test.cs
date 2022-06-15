using Senpai;

namespace Sample;

public partial class Program
{
    /// <summary>
    /// For testing purposes.
    /// </summary>
    /// <remarks>
    /// You might run into issues if you have an input that contains whitespaces. 
    /// Use the other method, if that happens.
    /// </remarks>
    private static void Invoke(string args, bool Execute = true, bool Block = false)
    {
        Invoke(string.IsNullOrWhiteSpace(args) ? new string[0] : args.Split((char)32), Execute, Block);
    }

    /// <summary>
    /// For testing purposes.
    /// </summary>
    private static void Invoke(string[] args, bool Execute = true, bool Block = false)
    {
        if (Execute)
        {
            Cli.Initialize(args);
            Thread.Sleep(Block ? -1 : 0); // Similar to Console.ReadLine() or Console.ReadKey
        }
    }
}