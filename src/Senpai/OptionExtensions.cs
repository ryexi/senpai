using System.Reflection;

namespace Senpai
{
    /// <summary>
    /// Provides extension methods for <see cref="Option"/>.
    /// </summary>
    internal static class OptionExtensions
    {
        public static Option ToOption(this OptionAttribute attribute, ParameterInfo parameter)
        {
            var symbol = attribute as ISymbolAttribute;
            var option = new Option(symbol.Name!, symbol.Description, parameter.ParameterType)
            {
                Arity      = symbol.Arity.Convert(),
                IsRequired = attribute.IsRequired,
                IsHidden   = attribute.IsHidden,
                Parameter  = parameter,
            };

            if (attribute.Alias != null)
                option.AddAlias(attribute.Alias);

            if (attribute.Aliases != null)
                for (int i = 0; i < attribute.Aliases.Length; i++)
                    option.AddAlias(attribute.Aliases[i]);

            return option;
        }
    }
}