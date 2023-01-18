namespace Senpai
{
    /// <summary>
    /// A symbol defining a value that can be passed on the command line to a <see cref="Command">command</see> or <see cref="Option">option</see>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class ArgumentAttribute : SymbolAttribute
    {
        public ArgumentAttribute(uint id, string name) 
        { 
            this.Index = id;
            this.Name = name;
        }

        internal uint Index
        {
            get;
            set;
        }

        /// <summary>
        /// The name used in help output to describe the argument. 
        /// </summary>
        public string? HelpName
        {
            get;
            set;
        }
    }
}