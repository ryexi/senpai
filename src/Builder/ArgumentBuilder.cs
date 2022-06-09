using System.CommandLine;
using System.CommandLine.Binding;
using System.Reflection;

namespace Senpai.Builder;

internal class ArgumentBuilder
{
    /// <summary>
    /// Build an <see cref="System.CommandLine.Option"/> object.
    /// </summary>
    /// <param name="Attribute">The metadata.</param>
    private static Option BuildOption(dynamic Attribute)
    {
        var GenericType     = typeof(Option<>).MakeGenericType(Attribute.GetGenericClassType());
        var CreateInstance  = Activator.CreateInstance(GenericType, new object?[] { Attribute.Name, Attribute.Description })!;
        var Option          = (Option)CreateInstance;

        Option.Arity = Convertible.ConvertArgumentArity(Attribute.Arity) ?? Option.Arity;

        // Adding aliases
        for (int i = 0; i < Attribute.Alias.Length; i++)
            Option.AddAlias(Attribute.Alias[i]);

        return Option;
    }

    /// <summary>
    /// Build an <see cref="System.CommandLine.Argument"/> object.
    /// </summary>
    /// <param name="Attribute">The metadata.</param>
    private static Argument BuildArgument(dynamic Attribute)
    {
        var GenericType     = typeof(Argument<>).MakeGenericType(Attribute.GetGenericClassType());
        var CreateInstance  = Activator.CreateInstance(GenericType, new object?[] { Attribute.Name, Attribute.Description })!;
        var Argument        = (Argument)CreateInstance;

        Argument.Arity = Convertible.ConvertArgumentArity(Attribute.Arity) ?? Argument.Arity;

        return (Argument)CreateInstance;
    }
    
    public static IValueDescriptor[] Build(Command Command,
                                           MethodInfo Method)
    {
        Attribute[]?        Options              = Convertible.GetGenericAttributes(Method, typeof(Token.Option<>));
        Attribute[]?        Arguments            = Convertible.GetGenericAttributes(Method, typeof(Token.Argument<>));
        ParameterInfo[]     Parameters           = Method.GetParameters();
        IValueDescriptor[]  ValueDescriptors     = new IValueDescriptor[Parameters.Length];
        dynamic[]           AttributeDescriptors = new dynamic[Parameters.Length];

        if (Parameters.Length != (Arguments.Length + Options.Length))
            throw new Exception("Arguments and/or options don't match the length of params.");

        if (Parameters.Length > 16)
            throw new Exception("Model binding more than 16 options and arguments is not supported.");

        /*
         * Converting the generic argument and option attributes to their underlying type.
         * And, sorting the attributes' placement.
         */
        {
            int Index     = 0;
            int Placement = 0;

            // Arguments
            for (int i = 0; i < Arguments.Length; i++, Index++)
            {
                AttributeDescriptors[Index] = Convertible.ConvertGenericAttribute(Arguments[i], typeof(Token.Argument<>));
                if (((uint?)AttributeDescriptors[Index].Index).HasValue)
                    Placement++;
            }

            // Options
            for (int i = 0; i < Options.Length; i++, Index++)
            {
                AttributeDescriptors[Index] = Convertible.ConvertGenericAttribute(Options[i], typeof(Token.Option<>));
                if (((uint?)AttributeDescriptors[Index].Index).HasValue)
                    Placement++;
            }

            if (Placement > 0 && Placement != Parameters.Length)
                throw new Exception("Missing indexes.");

            // Expecting the args to be in correlation with the method's params.
            if (Placement > 0)
                AttributeDescriptors = AttributeDescriptors.OrderBy(s => s.Index).ToArray();
        }

        /*
         * Retrieving information from the attributes and building the arguments and options.
         */
        {
            for (int i = 0; i < AttributeDescriptors.Length; i++)
            {
                ParameterInfo    Parameter           = Parameters[i];
                dynamic          AttributeDescriptor = AttributeDescriptors[i];
                IValueDescriptor ValueDescriptor;

                if (Parameter.ParameterType != AttributeDescriptor.GetGenericClassType())
                    throw new Exception("Type mismatch.");
                
                if (i < Arguments.Length) 
                {
                    ValueDescriptor = ValueDescriptors[i] = BuildArgument(AttributeDescriptor);
                    Command.AddArgument((Argument)ValueDescriptor);
                } 
                else 
                {
                    ValueDescriptor = ValueDescriptors[i] = BuildOption(AttributeDescriptor);
                    Command.AddOption((Option)ValueDescriptor);
                }
            }
        }

        return ValueDescriptors;
    }
}