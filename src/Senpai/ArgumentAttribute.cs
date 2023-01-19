namespace Senpai
{
    /// <summary>
    /// A symbol defining a value that can be passed on the command line to a <see cref="Command">command</see> or <see cref="Option">option</see>.
    /// </summary>
    public sealed class ArgumentAttribute : SymbolAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentAttribute"/> class.
        /// </summary>
        public ArgumentAttribute(uint id)
        {
            this.Index = id;
        }

        public ArgumentAttribute(uint id, string name)
        {
            this.Index = id;
            this.Name = name;
        }

        public ArgumentAttribute(uint id, string name, string description)
        {
            this.Index = id;
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// The name used in help output to describe the argument. 
        /// </summary>
        public string? HelpName
        {
            get;
            set;
        }

        internal uint Index
        {
            get;
            set;
        }
    }
}