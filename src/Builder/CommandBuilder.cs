using System.Reflection;
using System.CommandLine;

namespace Senpai.Builder;

internal static class CommandBuilder
{
    /// <summary>
    /// Build a root and its children.
    /// </summary>
    public static Command Build(MethodInfo Method)
    {
        /*
         * The "root" here is a Token.Command object which will be built into a System.CommandLine.Command object.
         * The root can have "children" which are Token.Verb objects, aka, verbs/subcommands.

         * When passed as a parameter to the method _Build(), 2 things occur.
         * The root gets built and if it has any children, it invokes the _Build() method and passes itself as a parent.
         * Coincidentally, if the children have children, they repeat the same cycle but when passing themselves, they include their parents.
         * Truly a mindfuck indeed.
         */
        return _Build(Method, (Token.Symbol)Method.GetCustomAttribute(typeof(Token.Command))!, false, new List<Command>());
    }

    private static Command _Build(MethodInfo Method, Token.Symbol Metadata, bool IsVerb, List<Command> Parents)
    {
        Command         Command;
        MethodInfo[]    Children;
        ParameterInfo[] Parameters;
        Internal.CaptureSource(Metadata.Source);

        if (!IsVerb && Method.GetCustomAttribute(typeof(Token.Verb)) != null)
            throw new Exception("Illegal Operation: [Verb] and [Command] cannot be on the same method.");
        if (IsVerb && Method.GetCustomAttribute(typeof(Token.Command)) != null)
            throw new Exception("Illegal Operation: [Verb] and [Command] cannot be on the same method.");

        Command    = new Command(Metadata.Name, Metadata.Description);
        Parameters = Method.GetParameters();
        Children   = Intermediate.GetMethods(Internal.CallingAssembly!, typeof(Token.Verb))
                                 .Where(m => ((Token.Verb)m.GetCustomAttribute(typeof(Token.Verb))!).Assignee == Metadata.Name)
                                 .ToArray();

        if (Parameters.Length == 0)
        {
            Command.SetHandler(() => Method.Invoke(null, new object[0]));
        }
        else
        {
            // Retrieves metadata of arguments and options and dynamically build them.
            var Params = ArgumentBuilder.Build(Method, Command, IsVerb, Parents);

            // Dynamically SetHandler
            InvocationBuilder.Build(Command, Params, Method);
        }

        if (Children.Length > 0)
            Parents.Add(Command);

        // Building sub-commands
        for (int i = 0; i < Children.Length; i++)
            Command.AddCommand(_Build(Children[i], (Token.Symbol)Children[i].GetCustomAttribute(typeof(Token.Verb))!, true, Parents));

        Internal.FreeCapturedSource();
        return Command;
    }
}