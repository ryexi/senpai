using System.Reflection;

namespace Senpai
{
    /// <summary>
    /// Provides extension methods for <see cref="Argument"/>.
    /// </summary>
    internal static class ArgumentExtensions
    {
        public static Argument ToArgument(this ArgumentAttribute attribute, ParameterInfo parameter)
        {
            var symbol = attribute as ISymbolAttribute;

            return new Argument(parameter.ParameterType)
            {
                Name        = symbol.Name ?? parameter.Name ?? parameter.ParameterType.ToString(),
                Description = symbol.Description,
                Arity       = symbol.Arity.Convert(),
                HelpName    = attribute.HelpName,
                IsHidden    = symbol.IsHidden,
                Parameter   = parameter,
            };
        }
    }
}