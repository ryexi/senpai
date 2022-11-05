namespace Sample.Commands
{
    [Command("sim")]
    public static class Simple
    {
        public static void Invoke() => Console.WriteLine("A very simple command.");
    }
}