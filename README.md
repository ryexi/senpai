A library that handles the command parsing, invocation and rendering of your cli app.

## Documentation
Read the [wiki](https://github.com/imdying/senpai/wiki/) and for more information about [dotnet-cli-api](https://github.com/dotnet/command-line-api), refer to the [official docs](https://github.com/dotnet/command-line-api/tree/v2.0.0-beta3.22114.1/docs).

## Under the hood
By retrieving metadata through reflection, Senpai is able to dynamically initialize the [dotnet-cli-api](https://github.com/dotnet/command-line-api) to act as its command-line interpreter/command-line processor. But all of this comes at the price of some [limitations](/LIMITS.md) and performance lost.

## Getting started
```C#
using Senpai;
using Senpai.Token;

static void Main(string[] args)
{
    Cli.Initialize(args);
}

[Command("mycmd", Description = "Its description.")]
public static void myMethod() 
{
    // my code
}
```

### A complex-ish command.
```C#
[Command(
    Name: "operation"
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