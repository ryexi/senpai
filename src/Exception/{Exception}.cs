namespace Senpai;

/// <summary>
/// Overriding the default class to provide more debugging information.
/// </summary>
internal partial class Exception : System.Exception
{
    public Exception(string message) : base(message)
    {
        this.SourceProvider = Internal.OpenSourceProvider;
    }

    private StackInfo? SourceProvider
    {
        get;
        set;
    }

    public override string? StackTrace
    {
        get
        {
            if (!SourceProvider.HasValue)
                return base.StackTrace;
            else
                return base.StackTrace + "\r\n   at " + SourceProvider?.Member + " in " + SourceProvider?.File + ":line " + SourceProvider?.Line;
        }
    }
}