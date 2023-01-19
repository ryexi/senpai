namespace Senpai.Invocation;

internal sealed class SafeThread
{
    private Thread _thread;
    private System.Timers.Timer _timer;

    public SafeThread(ThreadStart method)
    {
        _timer = new(250);
        _thread = new(method);

        // Calling 'Environment.Exit(...)' can cause the calling-thread/instantiated-thread
        // to get into an infinite 'WaitSleepJoin' state.
        // The root of the cause is from System.CommandLine.
        _timer.Elapsed += (s, e) =>
        {
            if (_thread.ThreadState == ThreadState.WaitSleepJoin)
                _thread.Interrupt();
        };

        _timer.Start();
        _thread.Start();

        // _thread.Join() blocks the calling thread until the thread is done.
        // If removed, the invocation from System.CommandLine would not take
        // an exit-code.
        _thread.Join();
    }
}