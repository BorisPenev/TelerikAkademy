using System;
using System.Linq;
using System.Text;

namespace ShoppingCenter
{
    public class Command
    {
        readonly char[] separators = { ';' };
        private const char COMMAND_END = ' ';

        public CommandTypes Type { get; set; }

        public string OriginalForm { get; set; }

        public string Name { get; set; }

        public string[] Parameters { get; set; }

        public Command(string input)
        {
            this.OriginalForm = input.Trim();
            this.Name = this.ParseName();
            this.Parameters = this.ParseParameters();
            this.Type = this.ParseCommandType(this.Name);
        }

        public CommandTypes ParseCommandType(string commandName)
        {
            if (commandName.Contains(COMMAND_END) || commandName.ContainsAny(separators))
            {
                throw new FormatException("Command Name cannot contains " + COMMAND_END + " or " + separators);
            }

            switch (commandName)
            {
                case "AddProduct":
                    return CommandTypes.AddProduct;
                case "DeleteProducts":
                    return CommandTypes.DeleteProducts;
                case "FindProductsByName":
                    return CommandTypes.FindProductsByName;
                case "FindProductsByPriceRange":
                    return CommandTypes.FindProductsByPriceRange;
                case "FindProductsByProducer":
                    return CommandTypes.FindProductsByProducer;
                default:
                    throw new ArgumentException("Invalid command name: " + commandName);
            }
        }

        public string ParseName()
        {
            var name = this.OriginalForm.Split(COMMAND_END)[0];
            return name;//.Trim();
        }

        public string[] ParseParameters()
        {
            var paramsOriginalForm = this.OriginalForm.Remove(0, this.Name.Length + 1);
            var parameters = paramsOriginalForm.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            //for (var i = 0; i < parameters.Length; i++)
            //{
            //    parameters[i] = parameters[i].Trim();
            //}
            return parameters;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(this.Name + " ");
            foreach (var param in this.Parameters)
            {
                sb.Append(param + " ");
            }

            return sb.ToString();
        }
    }
}