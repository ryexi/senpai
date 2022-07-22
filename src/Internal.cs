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
    /// CallerFilePathAttribute 
    /// </summary>
    /// <remarks>
    /// This property has to be manually updated in order to provide relevant info when throwing an exception of <see cref="Senpai.Exception"/>.
    /// </remarks>
    public static StackTraceObject? CapturedCaller
    {
        get;
        private set;
    }

    /// <summary>
    /// Captures the <see cref="Senpai.Exception.StackTraceObject"/> object of a <see cref="Senpai.Token.Command"/> object.
    /// </summary>
    public static void CaptureCaller(StackTraceObject source)
    {
        CapturedCaller = source;
    }

    /// <summary>
    /// Free <see cref="Senpai.Internal.CapturedSource"/>
    /// </summary>
    public static void FreeCapturedCaller()
    {
        CapturedCaller = null;
    }
}