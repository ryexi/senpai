namespace Senpai.Test.Commands;

public sealed class Exit : Command
{
    [Argument(1, "Code", "The exit code to return.")]
    public int Code
    {
        get;
        set;
    } = 0;

    protected override void Invocation(object?[] args)
    {
        Environment.Exit(Code);
    }
}
