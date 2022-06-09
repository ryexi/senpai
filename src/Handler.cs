using System.CommandLine;
using System.Reflection;
using Senpai.Builder;

namespace Senpai;

internal static class Handler
{
    private static RootCommand? Root
    {
        get;
        set;
    }

    public static int Invoke(string[] args, string? description, Assembly caller)
    {
        if ((Internal.CallingAssembly = caller) == null)
            throw new ArgumentNullException(nameof(caller));
		
        Root = new RootCommand();
        Root.Description = description ?? "No description provided.";

        // The executable itself isn't considered as a command.
        // As such, the default behavior will be printing the help menu.
        if (args.Length == 0)
            args = new string[] { "-h" };

        // Getting all the [Command] decorated methods from the Assembly.
        var RawCommands = Convertible.GetMethods(caller, typeof(Token.Command));

        // Unknown if I gotta do this or if there's already an option for that but whatever.
        RawCommands = RawCommands.OrderBy(m => ((Token.Command)m.GetCustomAttribute(typeof(Token.Command))!).Name)
                                 .ToArray();

        //! Performance goes brrr.
        for (int i = 0; i < RawCommands.Length; i++)
            Root.AddCommand(CommandBuilder.Build(RawCommands[i]));

        return Root.Invoke(args);
    }
}