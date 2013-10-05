using System;
using System.Linq;
using System.Text;

namespace CatalogOfFreeContent
{
    public class CommandExecutor : ICommandExecutor
    {
        public void ExecuteCommand(ICatalog catalog, ICommand command, StringBuilder output)
        {
            switch (command.Type)
            {
                case CommandTypes.AddBook:
                    AddBookCommand(catalog, command, output);
                    break;
                case CommandTypes.AddMovie:
                    AddMovieCommand(catalog, command, output);
                    break;
                case CommandTypes.AddSong:
                    AddSongCommand(catalog, command, output);
                    break;
                case CommandTypes.AddApplication:
                    AddApplicationCommand(catalog, command, output);
                    break;
                case CommandTypes.Update:
                    IsValidParameters(command);
                    UpdateCommand(catalog, command, output);
                    break;
                case CommandTypes.Find:
                    IsValidParameters(command);
                    FindCommand(catalog, command, output);
                    break;
                default:
                    throw new ArgumentException("Unknown command: " + command.Type.ToString());
            }
        }

        private static void FindCommand(ICatalog catalog, ICommand command, StringBuilder output)
        {
            var numberOfElementsToList = int.Parse(command.Parameters[1]);
            var contentList = catalog.GetContentByTitle(command.Parameters[0], numberOfElementsToList);
            if (contentList != null && contentList.Count() > 0)
            {
                foreach (IContent content in contentList)
                {
                    output.AppendLine(content.TextRepresentation);
                }
            }
            else
            {
                output.AppendLine("No items found");
            }
        }

        private static void UpdateCommand(ICatalog catalog, ICommand command, StringBuilder output)
        {
            var updatedCount = catalog.UpdateUrl(command.Parameters[0], command.Parameters[1]);
            output.AppendLine(String.Format("{0} items updated", updatedCount));
        }

        private static void AddApplicationCommand(ICatalog catalog, ICommand command, StringBuilder output)
        {
            catalog.Add(new Content(ContentTypes.Application, command.Parameters));
            output.AppendLine("Application added");
        }

        private static void AddSongCommand(ICatalog catalog, ICommand command, StringBuilder output)
        {
            catalog.Add(new Content(ContentTypes.Song, command.Parameters));
            output.AppendLine("Song added");
        }

        private static void AddMovieCommand(ICatalog catalog, ICommand command, StringBuilder output)
        {
            catalog.Add(new Content(ContentTypes.Movie, command.Parameters));
            output.AppendLine("Movie added");
        }

        private static void AddBookCommand(ICatalog catalog, ICommand command, StringBuilder output)
        {
            catalog.Add(new Content(ContentTypes.Book, command.Parameters));
            output.AppendLine("Book added");
        }

        private static void IsValidParameters(ICommand command)
        {
            if (command.Parameters.Length != 2)
            {
                throw new ArgumentOutOfRangeException("Invalid number of parameters for command " + command.Type.ToString());
            }
        }
    }
}