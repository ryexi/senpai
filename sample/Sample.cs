using Senpai;
using Senpai.Token;

namespace Sample;

public partial class Sample
{
    static void Main(string[] args)
    {
        // Default behavior.
        Cli.Initialize(args);                                                // Option 1
        //// Cli.Initialize(new string[0]);                                       // Option 2

        // Invoking a command.
        //// Cli.Initialize(new string[] { "test" });                             // Option 1
        //// Cli.Initialize(new string[] { "check", "test/item.txt" });           // Option 2

        // Invoking the help of a command.
        //// Cli.Initialize(new string[] { "check", "--help" });

        // Thread.Sleep(-1);
    }
}