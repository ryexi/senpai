namespace Senpai.Test.Commands;

public sealed class Crash : Command
{
    protected override void Invocation(object?[] args)
    {
        throw new Exception();
    }
}