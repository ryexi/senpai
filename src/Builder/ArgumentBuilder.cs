using System.CommandLine;
using System.CommandLine.Binding;
using System.Reflection;

namespace Senpai.Builder;

internal static class ArgumentBuilder
{
    /// <summary>
    /// Build an <see cref="System.CommandLine.Option"/> object.
    /// </summary>
    /// <param name="Attribute">The metadata.</param>
    private static Option BuildOption(dynamic Attribute)
    {
        var GenericType     = typeof(Option<>).MakeGenericType(Attribute.GetGenericType());
        var CreateInstance  = Activator.CreateInstance(GenericType, new object?[] { Attribute.Name, Attribute.Description })!;
        var Option          = (Option)CreateInstance;

        #region Properties
        if (Attribute.Arity != null)
            Option.Arity = Intermediate.ConvertEnum(Attribute.Arity);

        if (Attribute.Alias != null)
            Option.AddAlias(Attribute.Alias);

        if (Attribute.Aliases != null)
            for (int i = 0; i < Attribute.Aliases.Length; i++)
                Option.AddAlias(Attribute.Aliases[i]);

        Option.IsRequired = Attribute.IsRequired;
        #endregion

        return Option;
    }

    /// <summary>
    /// Build an <see cref="System.CommandLine.Argument"/> object.
    /// </summary>
    /// <param name="Attribute">The metadata.</param>
    private static Argument BuildArgument(dynamic Attribute)
    {
        var GenericType     = typeof(Argument<>).MakeGenericType(Attribute.GetGenericType());
        var CreateInstance  = Activator.CreateInstance(GenericType, new object?[] { Attribute.Name, Attribute.Description })!;
        var Argument        = (Argument)CreateInstance;


        #region Properties
        if (Attribute.Arity != null)
            Argument.Arity = Intermediate.ConvertEnum(Attribute.Arity);

        if (Attribute.HelpName != null)
            Argument.HelpName = Attribute.HelpName;
        #endregion

        return (Argument)CreateInstance;
    }

    /// <summary>
    /// The # which is all the arguments to process.
    /// </summary>
    private static int GetSizeOfParents(List<Command> Parents, out int Size)
    {
        for (int i = Size = 0; i < Parents.Count; i++)
            Size += Parents[i].Arguments.Count;
        return Size;
    }

    public static IValueDescriptor[] Build(MethodInfo Method, Command Command, bool IsVerb, List<Command> Parents)
    {
        // Zero if root or parents have no arg<T>
        // Non-zero if not root
        GetSizeOfParents(Parents, out int ParentsParamsSize);

        Attribute[]?        Options              = Intermediate.Attribute.GetGenericAttributes(Method, typeof(Token.Option<>));
        Attribute[]?        Arguments            = Intermediate.Attribute.GetGenericAttributes(Method, typeof(Token.Argument<>));
        ParameterInfo[]     Parameters           = Method.GetParameters();
        IValueDescriptor[]  ValueDescriptors     = new IValueDescriptor[Parameters.Length];
        dynamic[]           AttributeDescriptors;

        if (Parameters.Length != (Arguments.Length + Options.Length + ParentsParamsSize))
            throw new Exception("Parameters length mismatch.");

        if (Parameters.Length > 16)
            throw new Exception("Model binding more than 16 options and arguments is not supported.");

        /*
         * Converting the generic argument and option attributes to their underlying type.
         */
        {
            int Index = 0;
            AttributeDescriptors = new dynamic[Parameters.Length - ParentsParamsSize];

            for (int i = 0; i < Arguments.Length; i++, Index++)
                AttributeDescriptors[Index] = Intermediate.Attribute.ConvertType(Arguments[i], typeof(Token.Argument<>));

            for (int i = 0; i < Options.Length; i++, Index++)
                AttributeDescriptors[Index] = Intermediate.Attribute.ConvertType(Options[i], typeof(Token.Option<>));

            if (Parameters.Length > 0)
                AttributeDescriptors = AttributeDescriptors.OrderBy(s => s.Index).ToArray();
        }

        /*
         * Retrieving data from the attributes and building the arguments and options.
         * Additionally, if the command is a verb, append the args of the parents.
         */
        {
            int Index = ParentsParamsSize;

            if (IsVerb && ParentsParamsSize > 0)
            {
                for (int i = 0; i < Parents.Count; i++)
                {
                    var Parent = Parents[i];
                    var Argument = Parents[i].Arguments;

                    for (int c = 0; c < Argument.Count; c++)
                        ValueDescriptors[i] = Argument[c];
                }
            }

            for (int i = 0; i < AttributeDescriptors.Length; i++, Index++)
            {
                var Parameter = Parameters[i];
                var AttributeDescriptor = AttributeDescriptors[i];

                if (Parameter.ParameterType != AttributeDescriptor.GetGenericType())
                    throw new Exception(string.Format("The parameter '{0}' and argument '{1}' don't have a matching type.", Parameter.Name, AttributeDescriptor.Name));

                if (i < Arguments.Length)
                    Command.AddArgument((Argument)(ValueDescriptors[Index] = BuildArgument(AttributeDescriptor)));
                else
                    Command.AddOption((Option)(ValueDescriptors[Index] = BuildOption(AttributeDescriptor)));
            }
        }

        return ValueDescriptors;
    }
}