namespace Sample.Commands
{
    public sealed class Error : Command
    {
        protected override void Invocation(object?[] args)
        {
            Cli.WriteWarning("Beep boop.");
            Cli.WriteError("This command has thrown an error which stops everything and returns a '-1' exit code.");
            Cli.WriteLine("Nani!!");
        }
    }
}