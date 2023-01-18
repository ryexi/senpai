namespace Senpai
{
    /// <summary>
    /// Represents the base <see langword="class"/> of all symbols.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class SymbolAttribute : Symbol
    {
        /// <summary>
        /// Defines the arity of the argument.
        /// </summary>
        public ArgumentArity Arity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the argument is hidden.
        /// </summary>
        public bool IsHidden
        {
            get;
            set;
        }
    }
}