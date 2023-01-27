namespace Sample.Commands
{
    public sealed class Error : Command
    {
        protected override void Invocation(object?[] args)
        {
            App.WriteWarning("Beep boop.");
            App.WriteError("This command has thrown an error which stops everything and returns a '-1' exit code.");
            App.WriteLine("Nani!!");
        }
    }
}