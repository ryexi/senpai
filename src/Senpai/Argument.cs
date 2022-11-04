using System.Reflection;

namespace Senpai
{
    internal sealed class Argument : System.CommandLine.Argument, ISymbol
    {
        public Argument(Type type) 
        {
            ValueType = type;
        }

        public ParameterInfo? Parameter { get; set; }

        public override Type ValueType { get; set; }
    }
}