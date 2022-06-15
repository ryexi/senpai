A declarative way of building .NET consoles.

## Documentation
* <a href="https://github.com/imdying/senpai/wiki/">Wikipedia</a>
* <a href="https://github.com/dotnet/command-line-api/tree/v2.0.0-beta3.22114.1/docs">System.CommandLine</a>

## Getting started
```C#
using Senpai;
using Senpai.Token;

[Command(
    Name: "do",
    Description: "Do something."
)]
[Option<bool>(
    Id: 2
    Name: "--alt", 
    Description: "An option which alter something."
)]
[Argument<string>(
    Id: 1
    Name: "Input",
    Arity = ArgumentArity.ExactlyOne
)]
public static void myMethod(string Input, bool Alter) 
{
    // my code
}

static void Main(string[] args)
{
    Cli.Initialize(args, "This is my app's description.");
}
```

## Under the hood
Senpai is able to simplify the process of building a cli by making use of the [new generic attribute feature](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11#generic-attributes). 

A *little bit* of reflection-ing and Senpai is able to dynamically build and initialize the [System.CommandLine](https://github.com/dotnet/command-line-api) library to act as its command-line interpreter. But all of this comes at the price of some [limitations](/LIMITS.md) and performance lost.
