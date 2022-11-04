using Senpai.Properties;

namespace Senpai
{
    /// <summary>
    /// Represents an <see langword="option"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class OptionAttribute : Attribute, ISymbolAttribute
    {
        private readonly string? description;
        private readonly uint index;
        private string? name;

        /// <inheritdoc cref="OptionAttribute"/>
        /// <param name="Id"></param>
        /// <param name="Name">The name of the option.</param>
        public OptionAttribute(uint Id, string Name)
        {
            if (string.IsNullOrWhiteSpace(name = Name))
                throw new ArgumentNullException(nameof(Name));

            index = Id;
        }

        /// <inheritdoc cref="OptionAttribute(uint, string)"/>
        /// <param name="Description">The description of the option.</param>
        public OptionAttribute(uint Id, string Name, string? Description)
        {
            if (string.IsNullOrWhiteSpace(name = Name))
                throw new ArgumentNullException(nameof(Name));

            index = Id;
            description = Description;
        }

        /// <summary>
        /// A string that can be used on the command line to specify the option.
        /// </summary>
        /// <value></value>
        public string? Alias
        {
            get;
            set;
        }

        /// <summary>
        /// The set of strings that can be used on the command line to specify the option.
        /// </summary>
        public string[]? Aliases
        {
            get;
            set;
        }

        public ArgumentArity Arity
        {
            get;
            set;
        }

        string? ISymbolAttribute.Description => description ?? Resources.COMMAND_NO_DESCRIPTION;

        uint ISymbolAttribute.Index => index;

        public bool IsHidden
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates whether the option is required when its parent command is invoked.
        /// </summary>
        /// <remarks>
        /// When an option is required and its parent command is invoked without it, an error results
        /// </remarks>
        public bool IsRequired
        {
            get;
            set;
        }

        string? ISymbolAttribute.Name
        {
            get => name;
            set => name = value;
        }
    }
}