namespace Senpai;

/// <summary>
/// Overriding the default class to provide more debugging information.
/// </summary>
internal partial class Exception : System.Exception
{
    public Exception(string message) : base(message)
    {
        this._Source = Internal.GetCapturedSource();
    }

    private StackInfo? _Source
    {
        get;
        set;
    }

    public override string? StackTrace
    {
        get
        {
            if (!_Source.HasValue)
                return base.StackTrace;
            else
                return base.StackTrace + "\r\n   at " + _Source?.Member + " in " + _Source?.File + ":line " + _Source?.Line;
        }
    }
}