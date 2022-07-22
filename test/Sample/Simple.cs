namespace Test.Sample;

[Command(
    Name: "simple",
    Description = "I am simple description."
)]
public static class Simple
{
    public static void Invoke() =>  Console.WriteLine("I invented a new word! Plagiarism!");
}