using System.CommandLine.Invocation;

namespace Senpai
{
    internal static class Invocation
    {
        public static void Handle(Command command, InvocationContext context)
        {
            var objects = new List<object?>();

            for (int i = 0; i < command.Inheritance?.Count; i++)
                objects.Add(context.ParseResult.GetValueForArgument(command.Inheritance[i]));

            for (int i = 0; i < command.Arguments?.Count; i++)
                objects.Add(context.ParseResult.GetValueForArgument(command.Arguments[i]));
            
            for (int i = 0; i < command.Options?.Count; i++)
                objects.Add(context.ParseResult.GetValueForOption(command.Options[i]));

            command.Invoker?.Invoke(null, objects.ToArray());
        }
    }
}