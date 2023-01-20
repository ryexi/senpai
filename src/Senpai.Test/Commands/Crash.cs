namespace Senpai.Test.Commands;

public sealed class Crash : Command
{
    protected internal override void Invocation(object?[] args)
    {
        throw new NotImplementedException();
    }
}