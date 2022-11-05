namespace Senpai
{
    internal interface ISymbolAttribute
    {
        /// <summary>
        /// Defines the arity of an option or argument.
        /// </summary>
        public ArgumentArity Arity
        {
            get;
            set;
        }

        public string? Description
        {
            get;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the symbol is hidden.
        /// </summary>
        public bool IsHidden
        {
            get;
            set;
        }

        internal uint Index
        {
            get;
        }

        internal string? Name
        {
            get;
            set;
        }
    }
}