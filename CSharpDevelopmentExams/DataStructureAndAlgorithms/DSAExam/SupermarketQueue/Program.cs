using System;
using System.Collections.Generic;
using System.Text;
using Wintellect.PowerCollections;

namespace SupermarketQueue
{
    public class Program
    {
        private static readonly BigList<string> name = new BigList<string>();
        private static readonly Dictionary<string, int> findNames = new Dictionary<string, int>();

        public static void Main()
        {
#if DEBUG
            Console.SetIn(new System.IO.StreamReader("../../input.txt"));
#endif
            var output = new StringBuilder();

            var line = Console.ReadLine();
            while (line != "End")
            {
                var command = new Command(line);
                switch (command.Name)
                {
                    case "Append":
                        AppendCommand(command, output);
                        break;
                    case "Insert":
                        InsertCommand(command, output);
                        break;
                    case "Find":
                        FindCommand(command, output);
                        break;
                    case "Serve":
                        ServeCommand(command, output);
                        break;
                    default:
                        break;
                }

                line = Console.ReadLine();
            }

            Console.Write(output);
        }

        private static void AppendCommand(Command command, StringBuilder output)
        {
            name.Add(command.Param);
            if (!findNames.ContainsKey(command.Param))
            {
                findNames.Add(command.Param, 0);
            }
            findNames[command.Param]++;

            output.AppendLine("OK");
        }

        private static void InsertCommand(Command command, StringBuilder output)
        {
            if (command.Index >= 0 && name.Count >= command.Index)
            {
                name.Insert(command.Index, command.Param);
                if (!findNames.ContainsKey(command.Param))
                {
                    findNames.Add(command.Param, 0);
                }
                findNames[command.Param]++;
                output.AppendLine("OK");
            }
            else
            {
                output.AppendLine("Error");
            }
        }

        private static void FindCommand(Command command, StringBuilder output)
        {
            if (findNames.ContainsKey(command.Param))
            {
                output.AppendLine(findNames[command.Param].ToString());
            }
            else
            {
                output.AppendLine("0");
            }
        }

        private static void ServeCommand(Command command, StringBuilder output)
        {
            int count = int.Parse(command.Param);
            if (count > name.Count)
            {
                output.AppendLine("Error");
            }
            else
            {
                while (count > 0)
                {
                    output.Append(name[0] + " ");
                    if (findNames.ContainsKey(name[0]))
                    {
                        findNames[name[0]]--;
                    }
                    name.RemoveAt(0);
                    count--;
                }
                output.Append(Environment.NewLine);
            }
        }
    }

    public class Command
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public string Param { get; set; }

        public Command(string input)
        {
            var param = input.Split(' ');

            this.Name = param[0];
            this.Index = 0;
            if (param.Length == 2)
            {
                this.Param = param[1];
            }
            else
            {
                this.Index = int.Parse(param[1]);
                this.Param = param[2];
            }
        }
    }
}