namespace Senpai;

/// <summary>
/// Overriding the default class to provide more debugging information.
/// </summary>
internal partial class Exception : System.Exception
{
    private StackTraceObject? _source;

    public Exception(string message) : base(message)
    {
        this._source = Internal.CapturedCaller;
    }

    public override string? StackTrace
    {
        get
        {
            if (!_source.HasValue)
                return base.StackTrace;
            else
                return base.StackTrace + "\r\n   at " + _source?.Member + " in " + _source?.File + ":line " + _source?.Line;
        }
    }

    public struct StackTraceObject
    {
        public uint Line;
        public string? File;
        public string? Member;
    }
}