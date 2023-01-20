namespace Senpai.Invocation;

/// <summary>
/// Isolate and execute a <see cref="Command">command</see> on a thread.
/// </summary>
/// <remarks>
/// By doing this, we can catch the needed exceptions and prevent <see cref="Environment.Exit(int)"/> from hanging the process.
/// </remarks>
internal sealed class Threading
{
    private readonly Thread _thread;

    public Threading(ThreadStart method)
    {
        _thread = new(method);
        _thread.Start();
    }
}