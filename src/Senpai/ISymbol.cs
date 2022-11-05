using System.Reflection;

namespace Senpai
{
    internal interface ISymbol
    {
        public Type ValueType
        {
            get;
            set;
        }

        public ParameterInfo? Parameter
        {
            get;
            set;
        }
    }
}