using Xunit;
using Senpai;
namespace Unit;

// For more information about unit testing, see https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test
public class Testing
{
    [Fact]
    public void Test()
    {
        // Reading comments is hard on this thing.
       Cli.Initialize();
    }

    [Fact]
    public static void Method()
    {
        return;
    }
}