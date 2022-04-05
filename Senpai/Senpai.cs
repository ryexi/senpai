using System.Reflection;
using Senpai.Core;
namespace Senpai;

/// <summary>
/// The main entry for senpai.
/// </summary>
public static class Cli
{
    /// <summary>
    /// The arguments passed to the cli.
    /// </summary>
    public static string[] Args => Handler.Arguments ?? new string[0];

    /// <summary>
    /// The command/input handler.
    /// </summary>
    /// <param name="args">The args passed to the current process.</param>
    public static void Initialize(string[] args) => Handler.Initialize(args, Assembly.GetCallingAssembly());

    /// <summary>
    /// The command/input handler.
    /// </summary>
    /// <param name="args">The args passed to the current process.</param>
    /// <param name="caller">The assembly containing the methods with the command decorator.</param>
    public static void Initialize(string[] args, Assembly caller) => Handler.Initialize(args, caller);

    /// <summary>
    /// Override the string that gets printed when no input is given.
    /// </summary>
    public static void OverrideDefaultBehavior(string str) => Handler.Output.Default.Buffer = str;
}