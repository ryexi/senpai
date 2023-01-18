using System.CommandLine;
using System.Reflection;

namespace Senpai
{
    internal sealed class OptionProperty
    {
        public OptionProperty(PropertyInfo prop)
        {
            Property = prop;
            Symbol = (prop.GetCustomAttribute(typeof(OptionAttribute)) as OptionAttribute).ShouldNotBeNull();
            Argument = Create(prop, Symbol);
        }

        public Option Argument
        {
            get;
            set;
        }

        public PropertyInfo Property
        {
            get;
            set;
        }

        public OptionAttribute Symbol
        {
            get;
            set;
        }

        private static Option Create(PropertyInfo prop, OptionAttribute symbol)
        {
            var propType = typeof(Option<>).MakeGenericType(prop.PropertyType);
            var instance = (Activator.CreateInstance(propType, new object[] { symbol.Name, symbol.Description }) as Option)!;

            instance.Arity = symbol.Arity.ConvertTo();
            instance.IsHidden = symbol.IsHidden;

            if (!string.IsNullOrWhiteSpace(symbol.Alias))
                instance.AddAlias(symbol.Alias);

            for (int i = 0; i < symbol.Aliases?.Length; i++)
            {
                var value = symbol.Aliases[i];

                if (!string.IsNullOrWhiteSpace(value))
                    instance.AddAlias(value);
            }

            return instance;
        }
    }
}