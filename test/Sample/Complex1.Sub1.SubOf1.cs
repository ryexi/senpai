namespace Test.Sample;

public static partial class Complex1
{
    public static partial class Sub1
    {
        [Command("subOf1")]
        public static class SubOf1
        {
            public static void Invoke(string path, string test)
            {
                Console.WriteLine("The sub-command of a sub-command.\nPath: {0}\nTest: {1}", path, test);
            }
        }
    }
}