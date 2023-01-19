namespace Senpai
{
    /// <summary>
    /// A symbol defining a value that can be passed on the command line to a <see cref="Command">command</see> or <see cref="OptionAttribute">option</see>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public abstract class SymbolAttribute : Symbol
    {
        /// <summary>
        /// Defines the arity of the argument.
        /// </summary>
        public ArgumentArity Arity
        {
            get;
            set;
        }
    }
}