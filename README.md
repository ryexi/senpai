# Senpai
A simple command handler for cli applications.

## Quick Start
```C#
using Senpai;

[Command("mycommand", Description = "my command's description.")]
public static void myMethod() 
{
    // my code
}

static void Main(string[] args) 
{
    Cli.Initialize(args);
}
```

## Retrieving parameters
There are 2 ways to interact with parameters/arguments that are passed to the cli.

### 1. Accessing `Cli.Args`
```C#
var array = Cli.Args;
```

### 2. The parameters of a method
```C#
[Command("mycommand")]
public static void myMethod(int a, string? b, string c = null)
{
    // 'b' can have a value or be omitted as double-quotes ("")
    // 'c' can have a value or be omitted as nothing
}
```

## Supported Parameter Type
1. Int
2. String
3. Double

And nullable of mentioned types.

## Note
1. Methods decorated with the `Command` attribute must have the `public` and `static` modifier.
2. No support for options/flags, yet. (I'm lazy)