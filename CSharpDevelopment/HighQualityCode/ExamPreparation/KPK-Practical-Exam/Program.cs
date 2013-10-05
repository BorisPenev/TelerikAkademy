using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogOfFreeContent
{
    public class Program
    {
        public static void Main()
        {
            var output = new StringBuilder();
            var catalog = new Catalog();
            ICommandExecutor c = new CommandExecutor();

            var commands = ParseCommands();
            foreach (var command in commands)
            {
                c.ExecuteCommand(catalog, command, output);
            }

            Console.Write(output);
        }

        private static IEnumerable<ICommand> ParseCommands()
        {
            var commandList = new List<ICommand>();
            while (true)
            {
                var line = Console.ReadLine();
                if (line != null && line.Trim() == "End")
                {
                    break;
                }
                commandList.Add(new Command(line));
            }
            return commandList;
        }
    }
}