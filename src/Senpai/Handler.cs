using System.CommandLine;
using Maid.Invocation;

namespace Maid;

internal static class Handler
{

    public static void SetHandler(this Command command)  
        => command.UnderlyingCommand.SetHandler(context => new CommandHandler(new (command, context)).Invoke());
}