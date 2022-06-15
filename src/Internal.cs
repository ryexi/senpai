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

    /// <remarks>
    /// This field has to be manually updated in order to provide relevant info when throwing an exception of <see cref="Senpai.Exception"/>.
    /// </remarks>
    private static StackInfo? CapturedSource
    {
        get;
        set;
    }

    /// <summary>
    /// Captures the <see cref="Senpai.Exception.StackInfo"/> object of a <see cref="Senpai.Token.Command"/> object.
    /// </summary>
    public static void CaptureSource(StackInfo Source)
    {
        CapturedSource = Source;
    }

    /// <summary>
    /// Returns the <see cref="Senpai.Exception.StackInfo"/> object of a <see cref="Senpai.Token.Command"/> object.
    /// </summary>
    public static StackInfo? GetCapturedSource()
    {
        return CapturedSource;
    }

    /// <summary>
    /// Free <see cref="Senpai.Internal.CapturedSource"/>
    /// </summary>
    public static void FreeCapturedSource()
    {
        CapturedSource = null;
    }
}