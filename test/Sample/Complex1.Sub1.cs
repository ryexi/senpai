namespace Test.Sample;

public static partial class Complex1
{
    [Command(
        Name: "sub1",
        Description = "Create the file if it doesn't exist."
    )]
    public static partial class Sub1
    {
        /// <summary></summary>
        /// <param name="path">The argument from the parent command.</param>
        /// <param name="test">The argument of this sub-command.</param>
        [Argument<string>(
            Id: 2, 
            Name: "Test",
            Arity = ArgumentArity.ZeroOrOne
        )]
        [Option<bool>(
            Id: 3,
            Name: "--confirm",
            Alias = "-c"
        )]
        public static void Invoke(string path, string test, bool confirm)
        {
            if (!confirm)
            {
                Console.WriteLine("Sub1 was executed but no '--confirm' flag was specified.");
                return;
            }

            if (!File.Exists(path))
            {
                Console.WriteLine("Writing to the path: {0}", path);
                File.Create(path);
            }
            else
            {
                Console.WriteLine("File '{0}' already exists, skipping.", path);
            }
        }
    }
}