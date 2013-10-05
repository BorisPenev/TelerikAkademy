using System;

namespace Phonebook
{
    internal class Command
    {
        public CommandTypes Type { get; set; }

        public string[] Parameters { get; set; }

        public Command(string command)
        {
            string commandName;
            this.Parameters = ParseParameters(command, out commandName);
            this.Type = this.ParseCommandType(commandName);
        }

        private static string[] ParseParameters(string command, out string commandName)
        {
            //Bottleneck Found
            //Large commands can be parsed slowly.
            //Unresolved
            commandName = command.Substring(0, command.IndexOf('('));
            var parameters = command.Substring(commandName.Length + 1, command.Length - commandName.Length - 2);
            string[] paramArray = parameters.Split(',');
            for (int j = 0; j < paramArray.Length; j++)
            {
                paramArray[j] = paramArray[j].Trim();
            }

            return paramArray;
        }

        private CommandTypes ParseCommandType(string commandName)
        {
            switch (commandName)
            {
                case "AddPhone":
                    if (this.Parameters.Length < 2)
                    {
                        throw new ArgumentOutOfRangeException("commandName "+ commandName +" parameters need to more than one");
                    }
                    return CommandTypes.AddPhone;
                case "ChangePhone":
                    if (this.Parameters.Length != 2)
                    {
                        throw new ArgumentOutOfRangeException("commandName "+ commandName +" parameters need to be equal to two");
                    }
                    return CommandTypes.ChangePhone;
                case "List":
                    if (this.Parameters.Length != 2)
                    {
                        throw new ArgumentOutOfRangeException("commandName "+ commandName +" parameters need to be equal to two");
                    }
                    return CommandTypes.List;
                default:
                    throw new ArgumentException("Invalid commandName: " + commandName);
            }
        }
    }
}