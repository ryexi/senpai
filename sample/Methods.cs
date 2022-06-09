using Senpai.Token;

namespace Sample;

public static class Methods
{
    /// <summary>
    /// Command 1
    /// </summary>
    [Command("test", "Display a test message.")]
    public static void RandomMethod() => Console.WriteLine("A simple command.");

    [Command("add", "2 + 2 = 5")]
    [Option<int>(1, "--a", Description = "Value a")]
    [Option<int>(2, "--b", Description = "Value b")]
    public static void Add(int a, int b) => Console.WriteLine("{0} plus {1} equals {2}", a, b, a + b);

    /// <summary>
    /// Command 2
    /// </summary>
    [Argument<string>("message")]
    [Command("print", "Print a message.")]
    public static void Print(string mgs) => Console.WriteLine(mgs);

    /// <summary>
    /// Command 3
    /// </summary>
    [Command(
        Name:        "check",
        Description: "Validate the existence of a path."
    )]
    [Option<bool>(
        Name:        "--create",
        Alias        = new string[] { "-c" },
        Description  = "Create the path if it doesn't exist. (Fake option)"
    )]
    [Argument<string>(
        Name:        "path",
        Arity        = ArgumentArity.ZeroOrOne
    )]
    public static void CheckPath(string? path, bool create)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            Console.WriteLine("Error: No a valid path.");
            return;
        }

        if (Path.Exists(path))
        {
            Console.WriteLine("'{0}' is a valid path.", path);
        }
        else
        {
            Console.WriteLine("Error: '{0}' isn't a valid path.", path);
            Console.WriteLine(create ? "'--create' option was passed." : "No options passed.");
        }
    }
}