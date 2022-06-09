using System.CommandLine;
using System.Reflection;

namespace Senpai;

/// <summary>
/// Reflection-ing and transmuting uhm.. thing.
/// </summary>
internal static class Convertible
{
    /// <summary>
    /// Returns methods that contains an attribute of <see cref="System.Type"/>.
    /// </summary>
    /// <Remarks>Methods must have the 'public' and 'static' modifier.</Remarks>
    public static MethodInfo[] GetMethods(Assembly Caller, Type type)
    {
        return Caller?.GetTypes().SelectMany(t => t.GetMethods(BindingFlags.Public |
                                                               BindingFlags.Static |
                                                               BindingFlags.InvokeMethod))
                                  .Where(m => m.GetCustomAttributes(type, false).Length > 0)
                                  .ToArray() ?? new MethodInfo[0];
    }

    public static Attribute[] GetGenericAttributes(MemberInfo member, Type type)
    {
        return member.GetCustomAttributes().Where(a =>
        {
            var AttributeType = a.GetType();
            if (AttributeType.IsGenericType)
            {
                return AttributeType.GetGenericTypeDefinition() == type;
            }
            else
            {
                return false;
            }
        }).ToArray();
    }

    /// <summary>
    /// Converts a generic attribute to its underlying type.
    /// </summary>
    public static dynamic ConvertGenericAttribute(Attribute attribute, Type type)
    {
        var UnderlyingType = attribute.GetType().GetUnderlyingType()!;
        var GenericType = type.MakeGenericType(UnderlyingType);
        return (dynamic)Convert.ChangeType(attribute, GenericType);
    }

    public static ArgumentArity ConvertArgumentArity(Token.ArgumentArity ArityEnum)
    {
        switch (ArityEnum)
        {
            case Token.ArgumentArity.Default:
                return default;
                
            case Token.ArgumentArity.Zero:
                return ArgumentArity.Zero;

            case Token.ArgumentArity.ZeroOrOne:
                return ArgumentArity.ZeroOrOne;

            case Token.ArgumentArity.ExactlyOne:
                return ArgumentArity.ExactlyOne;

            case Token.ArgumentArity.ZeroOrMore:
                return ArgumentArity.ZeroOrMore;

            case Token.ArgumentArity.OneOrMore:
                return ArgumentArity.OneOrMore;

            default:
                return default;
        }
    }
}