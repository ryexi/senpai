A command-line interpreter for dotnet consoles.

## Documentation
<svg aria-hidden="true" height="16" viewBox="0 0 15 15" version="1.1" width="16" data-view-component="true" style="margin-right: 4px; margin-bottom: -2.2px;">
    <path fill="#8b949e" fill-rule="evenodd" d="M7.775 3.275a.75.75 0 001.06 1.06l1.25-1.25a2 2 0 112.83 2.83l-2.5 2.5a2 2 0 01-2.83 0 .75.75 0 00-1.06 1.06 3.5 3.5 0 004.95 0l2.5-2.5a3.5 3.5 0 00-4.95-4.95l-1.25 1.25zm-4.69 9.64a2 2 0 010-2.83l2.5-2.5a2 2 0 012.83 0 .75.75 0 001.06-1.06 3.5 3.5 0 00-4.95 0l-2.5 2.5a3.5 3.5 0 004.95 4.95l1.25-1.25a.75.75 0 00-1.06-1.06l-1.25 1.25a2 2 0 01-2.83 0z"></path>
</svg>
<a href="https://github.com/imdying/senpai/wiki/">Wikipedia</a>

<svg aria-hidden="true" height="16" viewBox="0 0 15 15" version="1.1" width="16" data-view-component="true" style="margin-right: 4px; margin-bottom: -2.2px;">
    <path fill="#8b949e" fill-rule="evenodd" d="M7.775 3.275a.75.75 0 001.06 1.06l1.25-1.25a2 2 0 112.83 2.83l-2.5 2.5a2 2 0 01-2.83 0 .75.75 0 00-1.06 1.06 3.5 3.5 0 004.95 0l2.5-2.5a3.5 3.5 0 00-4.95-4.95l-1.25 1.25zm-4.69 9.64a2 2 0 010-2.83l2.5-2.5a2 2 0 012.83 0 .75.75 0 001.06-1.06 3.5 3.5 0 00-4.95 0l-2.5 2.5a3.5 3.5 0 004.95 4.95l1.25-1.25a.75.75 0 00-1.06-1.06l-1.25 1.25a2 2 0 01-2.83 0z"></path>
</svg>
<a href="https://github.com/dotnet/command-line-api/tree/v2.0.0-beta3.22114.1/docs">System.CommandLine</a>

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