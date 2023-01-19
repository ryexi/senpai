using System.CommandLine;
using System.Reflection;
using Senpai.Properties;

namespace Senpai
{
    /// <summary>
    /// Provides a command-line interpreter for CLI applications.
    /// </summary>
    public static class App
    {
        private static RootCommand? _root;

        /// <summary>
        /// Initialize and start the command-line interpreter.
        /// </summary>
        /// <param name="context">Configure the behavior of the interpreter.</param>
        public static void Run(AppContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            if (context.Assembly is null)
                throw new ArgumentNullException(nameof(context));

            if (context.Arguments?.Length == 0)
                context.Arguments = new[] { "--help" };

            _root = new RootCommand(context.Description ?? Resources.SymbolNoDescriptionProvided);

            foreach ( var cmd in Services.GetCommands(context))
            {
                _root.Add(cmd);
            }

            _ = _root.Invoke(context.Arguments!);
        }
    }
}