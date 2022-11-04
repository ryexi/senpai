namespace Senpai
{
    /// <summary>
    /// Provides higher-level services (static methods) for common <see cref="Command"/> tasks.
    /// </summary>
    internal static class Services
    {
        /// <remarks>
        /// Starts from the top-leveled static classes.
        /// </remarks>
        public static Command[] GetCommands(AppContext context) => 
            Array.ConvertAll(context.Assembly.ExportedTypes.WhereCommand().ToArray(), t => t.ToCommand());
    }
}