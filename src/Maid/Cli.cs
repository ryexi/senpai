using System.CommandLine;
using System.Diagnostics;
using Maid.Properties;

namespace Maid
{
    /// <summary>
    /// Provides a command-line interpreter for command-line interface applications.
    /// </summary>
    public static class Cli
    {
        private static RootCommand? _root;

        /// <summary>
        /// Initialize and start the command-line interpreter.
        /// </summary>
        /// <param name="context">Configure the behavior of the interpreter.</param>
        public static int Run(AppContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            if (context.Assembly is null)
                throw new ArgumentNullException(nameof(context));

            if (context.Arguments?.Length == 0)
                context.Arguments = new[] { "--help" };

            _root = new RootCommand(context.Description ?? Resources.SymbolNoDescriptionProvided);

            foreach (var cmd in Services.GetCommands(context))
            {
                _root.Add(cmd);
            }

            return _root.Invoke(context.Arguments!);
        }

        /// <summary>
        /// Kill the process and return a non-zero exit code.
        /// </summary>
        public static void Interrupt() => Terminate(-1);

        /// <summary>
        /// Kill the current process by calling <see cref="Process.GetCurrentProcess().Kill()"/>
        /// </summary>
        /// <param name="code">
        /// The exit code of the process. The default value is 0 (zero), which indicates that the process completed successfully.
        /// </param>
        public static void Terminate(int code)
        {
            Environment.ExitCode = code;
            Process.GetCurrentProcess().Kill();
        }

        /// <summary>
        /// Writes an error message to the standard output stream and terminates the application.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public static void WriteError(object? value) => WriteRawError($"Error: {value}");

        /// <summary>
        /// <see cref="Console.WriteLine(object?)"/>.
        /// </summary>
        /// <param name="value"></param>
        public static void WriteLine(object? value) => WriteLine(value, Console.ForegroundColor);

        /// <inheritdoc cref="WriteError(object?)"/>
        public static void WriteRawError(object? value)
        {
            WriteLine(value, ConsoleColor.Red);
            Interrupt();
        }

        /// <inheritdoc cref="WriteWarning(object?)"/>
        public static void WriteRawWarning(object? value) => WriteLine(value, ConsoleColor.Yellow);

        /// <summary>
        /// Writes a warning message to the standard output stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public static void WriteWarning(object? value) => WriteRawWarning($"Warning: {value}");

        private static void WriteLine(object? value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ResetColor();
        }
    }
}