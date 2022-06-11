A command-line interpreter for dotnet consoles.

## Documentation
![](https://raw.githubusercontent.com/imdying/senpai/main/res/link.svg) <a href="https://github.com/imdying/senpai/wiki/">Wikipedia</a>
</br>
![](https://raw.githubusercontent.com/imdying/senpai/main/res/link.svg) <a href="https://github.com/dotnet/command-line-api/tree/v2.0.0-beta3.22114.1/docs">System.CommandLine</a>

## Getting started
```C#
using Senpai;
using Senpai.Token;

static void Main(string[] args)
{
    Cli.Initialize(args);
}

[Command("test")]
public static void myMethod() 
{
    // my code
}
```

### A complex-ish command.
```C#
[Command(
    Name: "operation",
    Description = "Does something."
)]
[Option<bool>(
    Name: "--delete", 
    Description = "Delete something."
)]
[Argument<string>(
    Name: "A user-input or pathname.", 
    Arity = ArgumentArity.ZeroOrOne
)]
public static void myMethod(string Input, bool Delete) 
{
    // my code
}
```

## Under the hood
By retrieving metadata through reflection, Senpai is able to dynamically initialize the [System.CommandLine](https://github.com/dotnet/command-line-api) library to act as its command-line interpreter. But all of this comes at the price of some [limitations](/LIMITS.md) and performance lost.