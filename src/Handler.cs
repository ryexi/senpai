// #define TEST

using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Help;
using System.CommandLine.Parsing;
using System.Reflection;
using Senpai.Builder;

namespace Senpai;

internal static class Handler
{
    private static string[]? _args;
    private static Parser? _parser;
    private static RootCommand? _root;
    private static CommandLineBuilder? _builder;

    public static int Invoke(string[] args, string? description, Assembly caller)
    {
        Command[] cmds;

        if ((Internal.CallingAssembly = caller) == null)
            throw new ArgumentNullException(nameof(caller));

        // no args = display help
        if (args.Length == 0)
            args = new string[] { "-h" };

        _args = args;
        _root = new RootCommand();
        _root.Description = description ?? "No description provided.";
        _builder = new CommandLineBuilder(_root).UseDefaults();
        _parser = _builder.Build();

        // sorting
        cmds = Build().OrderBy(c => c.Name).ToArray();

        for (int i = 0; i < cmds.Length; i++)
            _root.AddCommand(cmds[i]);

        return _parser.Invoke(_args);
    }

    private static Command[] Build()
    {
        var topLevelClasses = Operation.Reflection.GetStaticTopLevelClasses(Internal.CallingAssembly!);
        var commandClasses  = Operation.Reflection.GetClassesWithAttribute(topLevelClasses, typeof(Token.Command));
        var commandArray    = new Command[commandClasses.Length];

#if TEST
        Console.WriteLine("# of static classes found: {0}", topLevelClasses.Length);
        Console.WriteLine("# of those being commands: {0}\n", commandClasses.Length);
#endif

        for (int i = 0; i < commandClasses.Length; i++)
            commandArray[i] = BuildRecursion(commandClasses[i], new List<Command>());

        return commandArray;
    }

    private static Command BuildRecursion(Type @class, List<Command> parentList)
    {
        Type[] nestedClasses;
        Type[] nestedCommandClasses;

        var commandMeta = (Token.Command)@class.GetCustomAttribute(typeof(Token.Command))!;
        var commandMethod = @class.GetMethod("Invoke", BindingFlags.Public | BindingFlags.Static);

        // name, summary and description of command
        var command = new Command(commandMeta.Name,
                                  commandMeta.Description);

        // applying summary
        if (!string.IsNullOrEmpty(commandMeta.Summary))
        {
            _builder = _builder!.UseHelp(ctx => ctx.HelpBuilder.CustomizeSymbol(command, secondColumnText: commandMeta.Summary));
            _parser = _builder!.Build();
        }

#if TEST
        Console.WriteLine("Building '{0}' from '{1}'", commandMeta.Name, @class.FullName);
        Console.WriteLine("\tHas Invoke() method: {0}", commandMethod != null);
#endif

        // build setHandler
        if (commandMethod != null)
        {
            var method = commandMethod!;
            var @params = method.GetParameters();
            Internal.CaptureCaller(commandMeta.StackTrace);

            if (@params.Length == 0)
                command.SetHandler(() => method.Invoke(null, new object[0]));
            else
                // Retrieves the metadata of the arguments and options and dynamically build them.
                // Also, dynamically initialize SetHandler.
                InvocationBuilder.Build(command, Params: ArgumentBuilder.Build(method, command, parentList), method);

            Internal.FreeCapturedCaller();
        }

        // checking for sub-classes/sub-commands
        nestedClasses = @class.GetNestedTypes().Where(t => t.IsAbstract && t.IsSealed && t.IsClass).ToArray();
        nestedCommandClasses = Operation.Reflection.GetClassesWithAttribute(nestedClasses, typeof(Token.Command));

        // append the current command to the parentList
        if (nestedCommandClasses.Length > 0)
            parentList.Add(command);

        // "Did you mean: recursion"
        for (int i = 0; i < nestedCommandClasses.Length; i++)
            command.AddCommand(BuildRecursion(nestedCommandClasses[i], parentList));

        return command;
    }
}