namespace Maid.Sdk
{
    /// <summary>
    /// Provides a flexible abstraction for the implementation of a command.
    /// </summary>
    public abstract class Facade
    {
        /// <summary>
        /// Gets or sets a <see cref="bool"/> value that controls the visibility of outputs from <see cref="WriteLine(object?)"/> and similar methods.
        /// </summary>
        public virtual bool IsQuiet
        {
            get;
            set;
        }

        private bool IsVerbose => IsQuiet is false;

        /// <summary>
        /// If anything of the <see cref="WriteLine(object?)"/> and similar methods have written to the stdout.
        /// </summary>
        public bool HasWritten
        {
            get;
            private set;
        }

        public void WriteLine(object? value)
        {
            if (IsVerbose)
            {
                if (!HasWritten)
                    HasWritten = true;

                Console.WriteLine(value);
            }
        }

        public void WriteWarning(object? value)
        {
            if (IsVerbose)
            {
                if (!HasWritten)
                    HasWritten = true;

                Cli.WriteWarning(value);
            }
        }

        public void WriteError(object? value) => Cli.WriteError(value);
    }
}