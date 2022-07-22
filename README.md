An experimental "command-line interpreter".

## Documentation
* [Wikipedia](https://github.com/imdying/senpai/wiki/)
* [System.CommandLine](https://github.com/dotnet/command-line-api/tree/v2.0.0-beta3.22114.1/docs)

## Under the hood
Senpai dynamically build and initialize the [System.CommandLine](https://github.com/dotnet/command-line-api) library to act as its command-line interpreter. The sole reason it exists is to simplify the process of building and managing a cli application.

## Limitations
* The executable isn't considered as a command.
* No options for the root command.
* No global options.
* No support for model binding more than 16 options and arguments.
* Setting a default value for arguments and options isn't supported yet.
* Slow as fuck. (?)