using System.Reflection;

namespace Senpai;

public static class Cli
{
    /// <summary>
    /// Initiate the command-line interpreter.
    /// </summary>
    /// <param name="Args">The args of the application.</param>
    public static int Initialize(string[] Args!!) => Handler.Invoke(Args, null, Assembly.GetCallingAssembly());

    /// <summary>
    /// Initiate the command-line interpreter.
    /// </summary>
    /// <param name="Args">The args of the application.</param>
    /// <param name="Description">The description of the application.</param>
    public static int Initialize(string[] Args!!, string? Description) => Handler.Invoke(Args, Description, Assembly.GetCallingAssembly());

    /// <summary>
    /// Initiate the command-line interpreter.
    /// </summary>
    /// <param name="Args">The args of the application.</param>
    /// <param name="Description">The description of the application.</param>
    /// <param name="Caller">The assembly that contains the decorated methods.</param>
    public static int Initialize(string[] Args!!, string? Description, Assembly Caller!!) => Handler.Invoke(Args, Description, Caller);
}