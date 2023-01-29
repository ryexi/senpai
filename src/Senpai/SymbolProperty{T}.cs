using System.Reflection;

namespace Maid
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