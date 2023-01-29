namespace Maid.Test.Commands;

public sealed class Exit : Command
{
    [Argument(
        1, 
        "Code", 
        "The exit code to return.",
        Arity = ArgumentArity.ZeroOrOne)]
    public int Code
    {
        get;
        set;
    }

    protected internal override void Invocation(object?[] args)
    {
        Environment.Exit(Code);
    }
}