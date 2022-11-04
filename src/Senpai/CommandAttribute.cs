namespace Senpai
{
    /// <summary>
    /// Declares a <see langword="class"/> as a command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class CommandAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandAttribute"/> object.
        /// </summary>
        public CommandAttribute() {}

        /// <inheritdoc cref="CommandAttribute()"/>
        /// <param name="Name">The name of the command.</param>
        public CommandAttribute(string Name) : this(Name, null) {}

        /// <inheritdoc cref="CommandAttribute(string)"/>
        /// <param name="Summary">A (short) description of the command.</param>
        public CommandAttribute(string Name,
                                string? Summary)
        {
            this.Name        = Name;
            this.Description = Summary;
        }

        /// <inheritdoc cref="CommandAttribute(string, string?)"/>
        /// <param name="Synopsis">A brief summary of the command.</param>
        /// <param name="Description">A description of the command, shown in help.</param>
        public CommandAttribute(string Name,
                                string? Synopsis,
                                string? Description)
        {
            this.Name        = Name;
            this.Description = Description;
            this.Synopsis    = Synopsis;
        }

        internal string? Description
        {
            get;
            private set;
        }

        internal string? Synopsis
        {
            get;
            private set;
        }

        internal string? Name
        {
            get;
            private set;
        }
    }
}