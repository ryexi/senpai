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