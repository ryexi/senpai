using Senpai.Properties;

namespace Senpai
{
    /// <summary>
    /// A symbol defining a value that can be passed on the command line to a <see cref="Command">command</see> or <see cref="OptionAttribute">option</see>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public abstract class SymbolAttribute : Attribute, ISymbol
    {
        private string? _description;

        /// <summary>
        /// Defines the arity of the argument.
        /// </summary>
        public ArgumentArity Arity
        {
            get;
            set;
        }
        public string? Name
        {
            get;
            set;
        }

        public string? Description
        {
            get => _description ?? Resources.SymbolNoDescriptionProvided;
            set => _description = value;
        }

        public bool IsHidden
        {
            get;
            set;
        }
    }
}