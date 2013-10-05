using System.Text;

namespace CatalogOfFreeContent
{
    public interface ICommandExecutor
    {
        void ExecuteCommand(ICatalog catalog, ICommand command, StringBuilder output);
    }
}
