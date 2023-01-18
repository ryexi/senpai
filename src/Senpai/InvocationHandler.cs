using System.CommandLine.Invocation;

namespace Senpai
{
    internal class InvocationHandler
    {
        private readonly System.Timers.Timer _timer;
        private readonly Thread _thread;

        public InvocationHandler(Command command, InvocationContext context)
        {
            _timer  = new(250);
            _thread = new(() => Invoke(command, context));

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

        private void Invoke(Command command, InvocationContext context)
        {
            List<object?> args = new();

            foreach (var arg in command.GetParentArguments())
            {
                args.Add(context.ParseResult.GetValue(arg.Argument));
            }

            for (int i = 0; i < command.Arguments?.Length; i++)
            {
                var arg = command.Arguments[i];
                arg.Property.SetValue(command, context.ParseResult.GetValue(arg.Argument));
            }

            for (int i = 0; i < command.Options?.Length; i++)
            {
                var arg = command.Options[i];
                arg.Property.SetValue(command, context.ParseResult.GetValue(arg.Argument));
            }

            try
            {
                context.ExitCode = command.Invoke(args.ToArray());
            }
            catch (Exception)
            {
                context.ExitCode = -1;
                throw;
            }
        }
    }
}