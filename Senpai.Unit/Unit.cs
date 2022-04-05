using Xunit;
using Senpai;
using Unit.Lib;
namespace Unit;

// For more information about unit testing, see https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test
public class Test
{
    [Fact]
    public void Unit()
    {
        // Maidenless command. (no params)
        Cli.Initialize(new string[0]);

        // --Help
        //// Cli.Initialize(new string[] { "--help" });

        // Method2: Some complex shits.
        //// Cli.Initialize(new string[] { "update", "20", "22" });
        //// Cli.Initialize(new string[] { "update", "20", "22", "x" });
        //// Cli.Initialize(new string[] { "update", "20", "" });

        // Method2: Requesting usage
        //// Cli.Initialize(new string[] { "update", "--help" });
    }

    [Command("Test")]
    public static void Method1()
    {
        Console.WriteLine("Hell/Heaven");
    }

    [Command("Update", Description = "Update the app.")]
    public static void Method2(int a, int? b, double optional = 0)
    {
        Console.WriteLine(b == null);
        Console.WriteLine("This is a test from Method2\nValue 1: {0}\nValue 2: {1}\nCalculation: {2}\nOptional\\s: {3}",
                          a,
                          b,
                          BootlegMath.Add(a, b ?? 0),
                          optional);
    }

    [Command("Abigfuckingname", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc convallis metus a condimentum blandit. Donec tincidunt nibh eget ex lacinia, quis semper tortor dictum. Morbi consequat sit amet est non dapibus. Praesent neque dolor, lacinia id ultricies at, mollis eu diam. Vestibulum fringilla elit vel erat tincidunt vehicula. Praesent tristique, mi in iaculis mattis, ligula velit semper nunc, quis hendrerit.")]
    public static void Method3()
    {
        Console.WriteLine("A string. I swear I'm not lazy.");
    }
}