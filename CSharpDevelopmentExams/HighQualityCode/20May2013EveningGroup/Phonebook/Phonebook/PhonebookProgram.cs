using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook
{
    public class PhonebookProgram
    {
        public static void Main()
        {
            var output = new StringBuilder();
            var phonebookRepository = new PhonebookRepository();
            var commandExecutor = new CommandExecutor();

            var commands = ParseCommands();
            foreach (var command in commands)
            {
                commandExecutor.ExecuteCommand(phonebookRepository, command, output);
            }

            Console.Write(output);
        }

        private static IEnumerable<Command> ParseCommands()
        {
            var commandList = new List<Command>();
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