using System.Reflection;

namespace Maid.Parsing;

internal sealed class OptionConverter
{
    public static OptionProperty[] Convert(PropertyInfo[] props)
    {
        var pool = new List<OptionProperty>();

        foreach (var prop in props)
        {
            if (Attribute.IsDefined(prop, typeof(OptionAttribute)))
            {
                pool.Add(Parse(prop));
            }
        }

        return pool.ToArray();
    }

    private static OptionProperty Parse(PropertyInfo propInfo)
    {
        var attribute = propInfo.GetCustomAttribute<OptionAttribute>().ShouldNotBeNull();
        var symbol    = Argument<OptionAttribute>.Create(
            propInfo.PropertyType, attribute.Name ?? throw new ArgumentNullException(nameof(attribute.Name))
        );

        symbol.SetValues(attribute);

        return new OptionProperty(propInfo, attribute, (System.CommandLine.Option)symbol);
    }
}