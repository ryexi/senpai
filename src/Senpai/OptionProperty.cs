using System.CommandLine;
using System.Reflection;

namespace Senpai
{
    internal sealed class OptionProperty : SymbolProperty<OptionAttribute>
    {
        public OptionProperty(PropertyInfo prop, OptionAttribute attribute, Option symbol)
        {
            Argument  = symbol;
            Attribute = attribute;
            Property  = prop;
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

        public OptionAttribute Attribute
        {
            get;
            set;
        }
    }
}