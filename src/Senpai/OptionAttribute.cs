namespace Senpai
{
    /// <summary>
    /// Represents an <see langword="argument"/>.
    /// </summary>
    public sealed class OptionAttribute : SymbolAttribute
    {
        public OptionAttribute(string name)
        {
            this.Name = name;
        }

        public OptionAttribute(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        public string? Alias
        {
            get;
            set;
        }

        public string[]? Aliases
        {
            get;
            set;
        }
    }
}