using Senpai.Token;

namespace Sample.commands;

public static class Simple
{
    /// <summary>
    /// Represents a simple command.
    /// </summary>
    [Command(
        Name: "simple",
        Description: "I am a simple description."
    )]
    public static void SimpleCMD()
    {
        Console.WriteLine("I am but a simple command.");
    }

    /// <summary>
    /// Represents a simple command that takes 1 required input w/ 1 optional option.
    /// </summary>
    [Command(
        Name: "write",
        Description: "Write something to the stdout."
    )]
    [Argument<string>(
        Id: 1,
        Name: "input",
        Arity = ArgumentArity.ExactlyOne
    )]
    [Option<int>(
        Id: 2,
        Name: "--times",
        Description: "An option for something."
    )]
    public static void Write(string Mgs, int Times)
    {
        Console.WriteLine("Your message: {0}", string.IsNullOrWhiteSpace(Mgs) ? "NULL" : Mgs);
        Console.WriteLine("The value given to '--times': {0}", Times);
    }
}