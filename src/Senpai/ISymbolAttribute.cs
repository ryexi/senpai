namespace Senpai
{
    internal interface ISymbolAttribute
    {
        public string? Description
        {
            get;
        }

        /// <summary>
        /// Defines the arity of an option or argument.
        /// </summary>
        public ArgumentArity Arity
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

        public bool IsHidden
        {
            get;
            set;
        }
    }
}