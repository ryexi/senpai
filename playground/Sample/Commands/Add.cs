namespace Sample.Commands
{
    [Command(Name: "add", Summary: "Addition is one of the four basic operations of arithmetic.")]
    public static class Add
    {
        [Argument(1, Name: "Value 1")]
        [Argument(2, Name: "Value 2")]
        public static void Invoke(int a, int b) => Console.WriteLine("Result: {0}", a + b);
    }
}