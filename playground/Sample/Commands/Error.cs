namespace Sample.Commands
{
    public sealed class Error : Command
    {
        protected override void Invocation(object?[] args)
        {
            Cli.WriteWarning("Beep boop.");
            Cli.WriteError("Sad beep boop, app has written an error message.");
            Cli.WriteLine("Nani!!");
        }
    }
}