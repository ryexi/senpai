namespace Test.Sample;

[Command(
    Name: "complex1",
    Summary = "I am concise.",
    Description = "I am a very detailed description of the command."
)]
public static partial class Complex1
{
    [Argument<string>(
        Id: 1,
        Name: "Path"
    )]
    public static void Invoke(string path)
    {
        Console.WriteLine("Path specified: {0}", path);
        Console.WriteLine("Is path valid:  {0}", Path.Exists(path));
    }
}