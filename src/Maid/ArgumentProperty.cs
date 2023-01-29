using System.CommandLine;
using System.Reflection;

namespace Maid
{
    internal sealed class ArgumentProperty : SymbolProperty<ArgumentAttribute>
    {
        public ArgumentProperty(PropertyInfo prop, ArgumentAttribute attribute, Argument symbol)
        {
            Argument  = symbol;
            Attribute = attribute;
            Property  = prop;
            Position  = (int)Attribute.Index;
        }

        public Argument Argument
        {
            get;
            set;
        }

        public int Position
        {
            get;
            private set;
        }

        public PropertyInfo Property
        {
            get;
            set;
        }

        public ArgumentAttribute Attribute
        {
            get;
            set;
        }
    }
}