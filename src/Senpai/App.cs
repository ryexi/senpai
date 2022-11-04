using System.CommandLine;
using Senpai.Properties;

namespace Senpai
{
    /// <summary>
    /// Provides a command-line interpreter for CLI applications.
    /// </summary>
    public static class App
    {
        /// <summary>
        /// Initialize and start the command-line interpreter.
        /// </summary>
        /// <param name="context">The context used by the interpreter.</param>
        public static int Run(AppContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            if (context.Assembly is null)
                throw new ArgumentNullException(nameof(context), "Assembly is null.");

            var _cmds = Services.GetCommands(context);
            var _root = new RootCommand(context.Description ?? Resources.COMMAND_NO_DESCRIPTION);

            for (int i = 0; i < _cmds.Length; i++)
                _root.AddCommand(_cmds[i]);

            return _root.Invoke(
                context.Arguments.Length == 0 ? new[] { "--help" } : context.Arguments
            );
        }
    }
}