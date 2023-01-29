namespace Maid;

internal static class ArgumentArityExtensions
{
    public static InternalArgumentArity ConvertTo(this ArgumentArity arity)
    {
        return arity switch
        {
            ArgumentArity.Zero       => InternalArgumentArity.Zero,
            ArgumentArity.ZeroOrOne  => InternalArgumentArity.ZeroOrOne,
            ArgumentArity.ExactlyOne => InternalArgumentArity.ExactlyOne,
            ArgumentArity.ZeroOrMore => InternalArgumentArity.ZeroOrMore,
            ArgumentArity.OneOrMore  => InternalArgumentArity.OneOrMore,
            _ => default,
        };
    }
}