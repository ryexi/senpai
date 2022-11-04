using System.CommandLine.Invocation;

namespace Senpai
{
    internal static class Invocation
    {
        public static void Handle(Command command, InvocationContext context)
        {
            var op1 = context.ParseResult.GetValueForOption(command.Options.ToArray()[0]);
            Console.WriteLine(op1);
            Console.WriteLine(op1 is null);

            // invoke(params)
        }
    }
}