using System.Reflection;
using Senpai.Properties;

namespace Senpai
{
    /// <summary>
    /// Provides extension methods for <see cref="Command"/>.
    /// </summary>
    internal static class CommandExtensions
    {
        public static Command ToCommand(this Type @ref, List<Command>? ancestors = null)
        {
            var command  = @ref.GetCustomAttribute(typeof(CommandAttribute)).ToCommand(@ref)!;
            var children = new Lazy<Type[]>(() => @ref.GetNestedTypes().WhereCommand(true).ToArray());

            if ((command.Invoker = @ref.GetMethod(Resources.COMMAND_HANDLER_NAME)) != null)
                command.SetArguments(ancestors);

            if (children.Value.Length > 0)
            {
                (ancestors ??= new()).Add(command);
            }

            // "Did you mean: recursion"
            foreach (var child in children.Value)
                command.AddCommand(child.ToCommand(ancestors));

            return command;
        }

        /// <summary>
        /// Converts an <see langword="object"/> of the type <see cref="CommandAttribute"/> to <see cref="Command"/>.
        /// </summary>
        public static Command ToCommand(this Attribute? attr, Type @ref)
        {
            if (attr is not CommandAttribute command)
                throw new Exception($"{nameof(attr)} is not of {nameof(CommandAttribute)}.");

            if (command.Name is not null && string.IsNullOrWhiteSpace(command.Name))
                Internal.Error(@ref, "Name cannot be empty or contain whitespace.");

            return new Command
            {
                Name        = command.Name ?? @ref.Name.ToLower(),
                Description = command.Description ?? Resources.COMMAND_NO_DESCRIPTION,
                Synopsis    = command.Synopsis,
                Reference   = @ref
            };
        }

        /// <summary>
        /// If <see langword="x"/> is a valid <see cref="CommandAttribute"/> component.
        /// </summary>
        public static IEnumerable<Type> WhereCommand(this IEnumerable<Type> types, bool isNested = false)
            => types.Where(t =>
                t.IsClass
                && (t.IsNested == isNested)
                && t.IsAbstract
                && t.IsSealed
                && t.GetCustomAttributes(typeof(CommandAttribute), false).Length > 0);
    }
}