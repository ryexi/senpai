namespace Senpai;

internal static class ArgumentArityExtensions
{
    public static System.CommandLine.ArgumentArity ConvertTo(this ArgumentArity arity)
    {
        return arity switch
        {
            ArgumentArity.Zero => System.CommandLine.ArgumentArity.Zero,
            ArgumentArity.ZeroOrOne => System.CommandLine.ArgumentArity.ZeroOrOne,
            ArgumentArity.ExactlyOne => System.CommandLine.ArgumentArity.ExactlyOne,
            ArgumentArity.ZeroOrMore => System.CommandLine.ArgumentArity.ZeroOrMore,
            ArgumentArity.OneOrMore => System.CommandLine.ArgumentArity.OneOrMore,
            _ => default,
        };
    }
}