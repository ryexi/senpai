namespace Maid.Parsing;

internal static class CommandConverter
{
    public static InternalCommand Create(Command command)
    {
        var prop = command.Properties;

        if (string.IsNullOrWhiteSpace(prop.Name))
            prop.Name = command.DefaultName;

        return new InternalCommand(prop.Name!, prop.Synopsis, prop.Description);
    }
}