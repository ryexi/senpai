using System.CommandLine.Invocation;

namespace Senpai.Invocation;

internal sealed class CommandHandlerContext
{
    public CommandHandlerContext(Command command, InvocationContext context)
    {
        CommandContext = command;
        InvocationContext = context;
    }

    public Command CommandContext
    {
        get;
        private set;
    }

    public InvocationContext InvocationContext
    {
        get;
        private set;
    }
}
