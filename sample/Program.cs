namespace Sample;

public partial class Program
{
    static void Main(string[] args)
    {
        /*
         * Select one of the options below and set 'Execute:' to 'true' to execute the command.
         */

        // Option 1
        // Equivalent to Cli.Initialize(args)
        Invoke(args,
               Execute: true);

        // Option 2: Invoking a command.
        Invoke("simple",
               Execute: false);

        // Option 3: Invoking the help menu of a command.
        Invoke("write --help",
               Execute: false);

        // Option 4: Invoking a command with params/args.
        Invoke(new string[] { "write", "Hello, I am a message.", "--times", "5" },
               Execute: false);

        // Option 5: A complex command.
        Invoke("file --help",
               Execute: false);

        // Option 6: A rundown of invoking a verbs/subcommands of a command.
        Invoke("[parse] file test.txt new 1234 save",
               Execute: false);
    }
}