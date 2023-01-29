using System.Reflection;

namespace Maid
{
    /// <summary>
    /// Represents a context used by the interpreter.
    /// </summary>
    public sealed class AppContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppContext"/> object.
        /// </summary>
        public AppContext()
        {
            Arguments = Array.Empty<string>();
        }

        /// <inheritdoc cref="AppContext()"/>
        /// <param name="args">The arguments to process.</param>
        public AppContext(string[] args)
        {
            Arguments = args;
        }

        /// <inheritdoc cref="AppContext(string[])"/>
        /// <param name="args"></param>
        /// <param name="description">A description of the application.</param>
        public AppContext(string[] args, string? description)
        {
            Arguments = args;
            Description = description;
        }

        /// <summary>
        /// The arguments to process.
        /// </summary>
        public string[] Arguments
        {
            get;
            set;
        }

        /// <summary>
        /// The <see cref="System.Reflection.Assembly"/> which contains the attributed classes.
        /// </summary>
        public Assembly Assembly { get; set; } = Assembly.GetCallingAssembly();

#if DEBUG
        /// <summary>
        /// If set to <see langword="true"/>, the "contracts" aka commands, are cached.
        /// </summary>
        /// <remarks>
        /// The cache file is saved in the same directory of the assembly.
        /// </remarks>
        public bool Cache => throw new NotImplementedException();
#endif

        /// <summary>
        /// A description of the application.
        /// </summary>
        public string? Description
        {
            get;
            set;
        }
    }
}