using System.CommandLine;

namespace Senpai.Operation;

internal static class Cast
{
    public static ArgumentArity ConvertArity(Token.ArgumentArity ArityEnum)
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

    /// <summary>
    /// Converts a generic attribute to its underlying type.
    /// </summary>
    public static dynamic ConvertAttributeToType(Attribute attribute, Type attributeGenericType)
    {
        var underlyingType = Reflection.GetUnderlyingType(attribute.GetType())!;
        var genericType = attributeGenericType.MakeGenericType(underlyingType);
        return (dynamic)Convert.ChangeType(attribute, genericType);
    }
}