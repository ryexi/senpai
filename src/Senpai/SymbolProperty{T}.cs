using System.Reflection;

namespace Senpai
{
    internal interface SymbolProperty<T> where T : SymbolAttribute
    {
        public PropertyInfo Property
        {
            get;
            set;
        }

        public T Attribute
        {
            get;
            set;
        }
    }
}