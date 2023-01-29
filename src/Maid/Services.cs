namespace Maid
{
    /// <summary>
    /// Provides higher-level services (static methods) for common <see cref="Command"/> tasks.
    /// </summary>
    internal static class Services
    {
        public static InternalCommand[] GetCommands(AppContext context)
        {
            var derivedClassTypes = context.Assembly.ExportedTypes.WhereCommand();
            // var derivedClassInstances = new List<object>();
            var internalCommands = new List<InternalCommand>();

            foreach (var derivedClassType in derivedClassTypes)
            {
                var instance = (Load(derivedClassType) as Command)!;

                //derivedClassInstances.Add(
                //    instance
                //);

                internalCommands.Add(
                    instance.UnderlyingCommand
                );
            }

            return internalCommands.ToArray();
        }

        /// <summary>
        /// Instantiates a class that derives from <see cref="Command"/>, build its underlying command and etc.
        /// </summary>
        public static object Load(Type @ref, object? ancestor = null)
        {
            var derivedClass = Activator.CreateInstance(@ref)!;
            var derivedHeirs = @ref.GetNestedTypes().WhereCommand(true);
            var command = (derivedClass as Command)!;

            if (ancestor != null)
            {
                command.ParentClass = ancestor;
            }

            if (derivedHeirs.Any())
            {
                ancestor = derivedClass;
            }

            foreach (var subclass in derivedHeirs)
            {
                var subclassOb = Load(subclass, ancestor);
                var subcommand = (subclassOb as Command)!;

                command.UnderlyingCommand.Add(subcommand.UnderlyingCommand);
            }

            return derivedClass;
        }
    }
}