namespace CatalogOfFreeContent
{
    public interface ICommand
    {
        CommandTypes Type { get; set; }

        string OriginalForm { get; set; }

        string Name { get; set; }

        string[] Parameters { get; set; }

        CommandTypes ParseCommandType(string commandName);

        string ParseName();

        string[] ParseParameters();
    }
}
