namespace Sample.Commands
{
    [Command(
        Name:        "com", 
        Synopsis:    "A short description.", 
        Description: "A long and detailed description of the command."
    )]
    public static class Complicated
    {
        [Argument(1)]
        public static void Invoke(string value) => Console.WriteLine("Argument passed: {0}", value);


        [Command]
        public static class Child
        {
            [Option(2, "--b", Arity = ArgumentArity.ExactlyOne)]
            public static void Invoke(string a, int b) => Console.WriteLine("From parent: {0}\nAdditionals: {1}", a, b);
        }
    }
}