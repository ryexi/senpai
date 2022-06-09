using System.Reflection;
using System.CommandLine;

namespace Senpai.Builder;

internal static class CommandBuilder
{
    public static Command Build(MethodInfo Method)
    {
        var Metadata              = (Token.Command)Method.GetCustomAttribute(typeof(Token.Command))!;
        var Command               = new Command(Metadata.Name, Metadata.Description);
        var Parameters            = Method.GetParameters();
        Internal.OpenSourceProvider = Metadata.SourceProvider;

        if (Parameters.Length == 0)
        {
            Command.SetHandler(() => Method.Invoke(null, new object[0]));
        }
        else
        {
            // Retrieves metadata of arguments and options and dynamically build them.
            // Returns an array of IValueDescriptor.
            var Params = ArgumentBuilder.Build(Command, Method);

            // Dynamically SetHandler
            InvocationBuilder.Build(Command, Params, Method);
        }

        // OpenSourceProvider = null
        Internal.ReleaseSourceProvider();

        return Command;
    }
}