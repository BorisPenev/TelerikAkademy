using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ShoppingCenter
{
    public class Program
    {
        public static void Main()
        {
            var output = new StringBuilder();
            var catalog = new Catalog();
            var c = new CommandExecutor();

            var commands = ParseCommands();
            foreach (var command in commands)
            {
                c.ExecuteCommand(catalog, command, output);
            }

            Console.Write(output);
        }

        private static IEnumerable<Command> ParseCommands()
        {
            var commandList = new List<Command>();
#if DEBUG
            string line;
            using (StreamReader reader = new StreamReader(@"..\..\test1.txt"))
            {
                line = reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    commandList.Add(new Command(line));
                }
            }
#else
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                commandList.Add(new Command(Console.ReadLine()));
            }
#endif

            return commandList;
        }
    }
}