namespace Maid
{
    public interface ISymbol
    {
        /// <summary>
        /// The name of the symbol.
        /// </summary>
        public abstract string? Name
        {
            get;
            set;
        }

        /// <summary>
        /// The description of the symbol, shown in help.
        /// </summary>
        public abstract string? Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the symbol is hidden.
        /// </summary>
        public abstract bool IsHidden
        {
            get;
            set;
        }
    }
}