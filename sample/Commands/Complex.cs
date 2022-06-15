using Senpai.Token;

namespace Sample.Commands;

public static class Complex
{
    /// <summary>
    /// Represents a complex command.
    /// </summary>
    [Command(
        Name: "file",
        Description: "I am big headache."
    )]
    [Argument<string>(
        Id: 1,
        Name: "Path",
        Arity = ArgumentArity.ExactlyOne
    )]
    [Option<bool>(
        Id: 2,
        Name: "--exist",
        Description: "Check if the path is valid."
    )]
    public static void ComplexCMD(string path, bool exist)
    {
        Console.WriteLine("Path: {0}", path);
        Console.WriteLine("Path Evalution: {0}", exist ? File.Exists(path) ? "Exists" : "Doesn't exist" : "Not defined.");
    }

    /// <summary>
    /// A subcommand of ComplexCMD
    /// </summary>
    [Verb(
        "delete",
        "Delete the file.",
        Assignee: "file"   // Must match the name of the parent command.
    )]
    public static void Delete(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    /// <summary>
    /// A subcommand of ComplexCMD
    /// </summary>
    [Verb(
        "new",
        "Create a file.",
        Assignee: "file"
    )]
    [Argument<string>(
        Id: 2,
        Name: "Content",
        Arity = ArgumentArity.ExactlyOne
    )]
    public static void New(string path, string content) 
    {
        Console.WriteLine("Append the sub-command 'save' after this command to actually save.\nPath: {0}\nContent: {1}", path, content);
    }

    /// <summary>
    /// A subcommand of New
    /// </summary>
    [Verb(
        "save",
        "Save a file.",
        Assignee: "new"
    )]
    public static void Save(string path, string content) 
    {
        File.WriteAllText(path, content);
    }
}