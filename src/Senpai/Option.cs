using System.Reflection;

namespace Senpai
{
    internal sealed class Option : System.CommandLine.Option, ISymbol
    {
        public Option(string name, string? description, Type type) : base(name, description)
        {
            ValueType = type;
        }

        public ParameterInfo? Parameter { get; set; }

        public override Type ValueType
        {
            get => base.ValueType;
            set => base.ValueType = value;
        }
    }
}