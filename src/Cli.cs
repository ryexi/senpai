using System.Reflection;
namespace Senpai;

public static class Cli
{
    /// <summary>
    /// Initiate the parsing, invocation and rendering of the cli app.
    /// </summary>
    /// <param name="args">The args of the application.</param>
    public static int Initialize(string[] args!!) => Handler.Invoke(args, null, Assembly.GetCallingAssembly());

    /// <summary>
    /// Initiate the parsing, invocation and rendering of the cli app.
    /// </summary>
    /// <param name="args">The args of the application.</param>
    /// <param name="description">The description of the application.</param>
    public static int Initialize(string[] args!!, string description) => Handler.Invoke(args, description, Assembly.GetCallingAssembly());

    /// <summary>
    /// Initiate the parsing, invocation and rendering of the cli app.
    /// </summary>
    /// <param name="args">The args of the application.</param>
    /// <param name="description">The description of the application.</param>
    /// <param name="caller">The assembly that contains the decorated methods.</param>
    public static int Initialize(string[] args!!, string description, Assembly caller!!) => Handler.Invoke(args, description, caller);
}