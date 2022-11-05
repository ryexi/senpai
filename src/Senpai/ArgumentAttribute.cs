using Senpai.Properties;

namespace Senpai
{
    /// <summary>
    /// Represents an <see langword="argument"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class ArgumentAttribute : Attribute, ISymbolAttribute
    {
        private readonly string? description;
        private readonly uint index;
        private string? name;

        /// <inheritdoc cref="ArgumentAttribute"/>
        /// <param name="Id">The position of its parameter.</param>
        public ArgumentAttribute(uint Id)
        {
            index = Id;
        }

        /// <inheritdoc cref="ArgumentAttribute(uint)"/>
        /// <param name="Id"></param>
        /// <param name="Name">The name of the argument</param>
        public ArgumentAttribute(uint Id, string Name)
        {
            if (string.IsNullOrWhiteSpace(name = Name))
                throw new ArgumentNullException(nameof(Name));

            index = Id;
        }

        /// <inheritdoc cref="ArgumentAttribute(uint, string)"/>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="Description">The description of the argument.</param>
        public ArgumentAttribute(uint Id, string Name, string? Description)
        {
            if (string.IsNullOrWhiteSpace(name = Name))
                throw new ArgumentNullException(nameof(Name));

            index = Id;
            description = Description;
        }

        /// <inheritdoc cref="ISymbolAttribute.Arity"/>
        public ArgumentArity Arity
        {
            get;
            set;
        }

        string? ISymbolAttribute.Description => description ?? Resources.COMMAND_NO_DESCRIPTION;

        /// <summary>
        /// The name used in help output to describe the argument.
        /// </summary>
        public string? HelpName
        {
            get;
            set;
        }

        uint ISymbolAttribute.Index => index;

        /// <inheritdoc cref="ISymbolAttribute.IsHidden"/>
        public bool IsHidden
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