using Senpai.Properties;

namespace Senpai
{
    /// <summary>
    /// A symbol defining a value that can be passed on the command line to a <see cref="Command">command</see> or <see cref="Option">option</see>.
    /// </summary>
    public class Symbol : Attribute
    {
        private string? _description;
        private string? _name;
        private string? _synopsis;

        /// <summary>
        /// The description of the symbol, shown in help.
        /// </summary>
        public string Description
        {
            get => _description ?? Resources.SymbolNoDescriptionProvided;
            set => _description = value;
        }

        /// <summary>
        /// The name of the symbol.
        /// </summary>
        public string Name
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

        /// <summary>
        /// The summary of the symbol, shown in help.
        /// </summary>
        public string? Synopsis
        {
            get => _synopsis;
            set => _synopsis = value;
        }
    }
}