using System.CommandLine;
using System.Reflection;

namespace Senpai
{
    internal sealed class ArgumentProperty
    {
        public ArgumentProperty(PropertyInfo prop)
        {
            Property = prop;
            Symbol = (prop.GetCustomAttribute(typeof(ArgumentAttribute)) as ArgumentAttribute).ShouldNotBeNull();
            Position = (int)Symbol.Index;
            Argument = Create(prop, Symbol);
        }

        public Argument Argument
        {
            get;
            set;
        }

        public PropertyInfo Property
        {
            get;
            set;
        }

        public ArgumentAttribute Symbol
        {
            get;
            set;
        }

        public int Position
        {
            get;
            private set;
        }

        private static Argument Create(PropertyInfo prop, ArgumentAttribute symbol)
        {
            var propType = typeof(Argument<>).MakeGenericType(prop.PropertyType);
            var instance = (Activator.CreateInstance(propType, new object[] { symbol.Name, symbol.Description }) as Argument)!;

            instance.Arity    = symbol.Arity.ConvertTo();
            instance.IsHidden = symbol.IsHidden;
            instance.HelpName = symbol.HelpName;

            return instance;
        }
    }
}