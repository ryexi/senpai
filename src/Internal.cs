using System.Reflection;
using static Senpai.Exception;

namespace Senpai;

internal static class Internal
{
    /// <summary>
    /// Returns the Assembly that invoked the Cli.Initialize(...) method.
    /// </summary>
    public static Assembly? CallingAssembly
    {
        get;
        set;
    }

    /// <summary>
    /// Returns the <see cref="Senpai.Exception.StackInfo"/> object of a <see cref="Senpai.Token.Command"/> object.
    /// </summary>
    /// <remarks>This field has to be manually updated in order to provide relevant info when throwing an exception of <see cref="Senpai.Exception"/>.</remarks>
    public static StackInfo? OpenSourceProvider
    {
        get;
        set;
    }

    public static void ReleaseSourceProvider()
    {
        OpenSourceProvider = null;
    }
}