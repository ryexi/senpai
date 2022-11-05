using System.CommandLine;
using System.Reflection;

namespace Senpai
{
    internal sealed class Command : System.CommandLine.Command
    {
        public new List<Argument>? Arguments
        {
            get;
            private set;
        }

        public override string? Description
        {
            get => base.Description;
            set => base.Description = value;
        }

        public List<Argument>? Inheritance
        {
            get;
            private set;
        }

        public MethodInfo? Invoker
        {
            get;
            set;
        }

        public override string Name
        {
            get => base.Name;
            set => base.Name = value;
        }

        public new List<Option>? Options
        {
            get;
            private set;
        }

        public Type? Reference
        {
            get;
            set;
        }

        public new List<Command>? Subcommands
        {
            get;
            set;
        }

        public override string? Synopsis
        {
            get => base.Synopsis;
            set => base.Synopsis = value;
        }

        public void AddCommand(Command command)
        {
            (Subcommands ??= new()).Add(command);
            base.AddCommand(command);
        }

        public void SetArguments(List<Command>? ancestors = null)
        {
            if (Invoker is null)
                throw new NullReferenceException(nameof(Reference));

            var parameters = Invoker.GetParameters();
            var @arguments = Invoker.GetCustomAttributes(attributeType: typeof(ArgumentAttribute))
                                    .Union(Invoker.GetCustomAttributes(attributeType: typeof(OptionAttribute)))
                                    .OrderBy(a => ((ISymbolAttribute)a).Index)
                                    .ToArray();
            var sizeofArgs = (ancestors?[ancestors.Count - 1].Arguments?.Count ?? 0) + (ancestors?[ancestors.Count - 1].Inheritance?.Count ?? 0);

            if (parameters.Length != (arguments.Length + sizeofArgs))
                Internal.Throw(Reference!, "Length of parameters and arguments are not equal.");

            #region Inheriting the args of the ancestors
            // Inheriting the args of the ancestors.
            for (int i = 0; i < ancestors?[ancestors.Count - 1].Inheritance?.Count; i++)
                (Inheritance ??= new()).Add(ancestors![ancestors.Count - 1].Inheritance![i]);

            for (int i = 0; i < ancestors?[ancestors.Count - 1].Arguments?.Count; i++)
                (Inheritance ??= new()).Add(ancestors![ancestors.Count - 1].Arguments![i]);
            #endregion

            foreach (var argument in arguments)
            {
                ParameterInfo param;
                try
                {
                    param = parameters[((ISymbolAttribute)argument).Index - 1];
                }
                catch (IndexOutOfRangeException)
                {
                    Internal.Throw(Reference!, "Index correlation error.");
                    throw;
                }

                if (argument is ArgumentAttribute arg)
                    AddArgument(arg.ToArgument(param));

                if (argument is OptionAttribute option)
                    AddOption(option.ToOption(param));
            }

            this.SetHandler((context) => Invocation.Handle(this, context));
        }

        private void AddArgument(Argument argument)
        {
            (Arguments ??= new()).Add(argument);
            base.AddArgument(argument);
        }

        private void AddOption(Option argument)
        {
            (Options ??= new()).Add(argument);
            base.AddOption(argument);
        }
    }
}