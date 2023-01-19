using Senpai.Properties;

namespace Senpai
{
    /// <summary>
    /// A symbol, such as an option or command, having one or more fixed names in a command line interface.
    /// </summary>
    public abstract class Symbol : Attribute
    {
        private string? _description;
        private string? _name;

        /// <summary>
        /// The description of the symbol, shown in help.
        /// </summary>
        public virtual string Description
        {
            get => _description ?? Resources.SymbolNoDescriptionProvided;
            set => _description = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the symbol is hidden.
        /// </summary>
        public bool IsHidden
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the symbol.
        /// </summary>
        public virtual string Name
        {
            get => _name ?? throw new ArgumentNullException(nameof(Name));

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _name = value;
            }
        }
    }
}