using System.CommandLine;

namespace Senpai.Parsing
{
    internal static class SymbolExtensions
    {
        public static void SetValues<T>(this Symbol symbol, T attribute) where T : SymbolAttribute
        {
            symbol.IsHidden = attribute.IsHidden;
            symbol.Description = attribute.Description;
            
            if (symbol is Argument argument && attribute is ArgumentAttribute aData)
            {
                argument.Arity = aData.Arity.ConvertTo();
                argument.HelpName = aData.HelpName;
            }
            
            if (symbol is Option option && attribute is OptionAttribute oData)
            {
                option.Arity = oData.Arity.ConvertTo();

                #region Aliases
                if (!string.IsNullOrWhiteSpace(oData.Alias))
                    option.AddAlias(oData.Alias);

                for (int i = 0; i < oData.Aliases?.Length; i++)
                {
                    var value = oData.Aliases[i];

                    if (!string.IsNullOrWhiteSpace(value))
                        option.AddAlias(value);
                }
                #endregion
            }
        }
    }
}