namespace Senpai
{
    internal static class ArgumentArityExtensions
    {
        public static System.CommandLine.ArgumentArity Convert(this ArgumentArity arity)
        {
            switch (arity)
            {
                case ArgumentArity.Default:
                    return default;
                case ArgumentArity.Zero:
                    return System.CommandLine.ArgumentArity.Zero;
                case ArgumentArity.ZeroOrOne:
                    return System.CommandLine.ArgumentArity.ZeroOrOne;
                case ArgumentArity.ExactlyOne:
                    return System.CommandLine.ArgumentArity.ExactlyOne;
                case ArgumentArity.ZeroOrMore:
                    return System.CommandLine.ArgumentArity.ZeroOrMore;
                case ArgumentArity.OneOrMore:
                    return System.CommandLine.ArgumentArity.OneOrMore;
                default:
                    return default;
            }
        }
    }
}
