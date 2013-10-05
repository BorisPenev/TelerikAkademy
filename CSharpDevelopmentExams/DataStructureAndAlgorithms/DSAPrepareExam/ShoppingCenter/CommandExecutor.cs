using System;
using System.Linq;
using System.Text;

namespace ShoppingCenter
{
    public class CommandExecutor
    {
        public void ExecuteCommand(Catalog catalog, Command command, StringBuilder output)
        {
            switch (command.Type)
            {
                case CommandTypes.AddProduct:
                    AddProductCommand(catalog, command, output);
                    break;
                case CommandTypes.DeleteProducts:
                    DeleteProductsCommand(catalog, command, output);
                    break;
                case CommandTypes.FindProductsByName:
                    FindProductsByNameCommand(catalog, command, output);
                    break;
                case CommandTypes.FindProductsByPriceRange:
                    FindProductsByPriceRangeCommand(catalog, command, output);
                    break;
                case CommandTypes.FindProductsByProducer:
                    FindProductsByProducerCommand(catalog, command, output);
                    break;
                default:
                    throw new ArgumentException("Unknown command: " + command.Type.ToString());
            }
        }

        private static void AddProductCommand(Catalog catalog, Command command, StringBuilder output)
        {
            catalog.Add(new Content()
                {
                    Name = command.Parameters[(int) ContentItemTypes.Name],
                    Price = double.Parse(command.Parameters[(int) ContentItemTypes.Price]),
                    Producer = command.Parameters[(int) ContentItemTypes.Producer]
                });
            output.AppendLine("Product added");
        }

        private static void DeleteProductsCommand(Catalog catalog, Command command, StringBuilder output)
        {
            int updatedCount = 0;

            if (command.Parameters.Length > 1)
            {
                updatedCount = catalog.Delete(command.Parameters[0], command.Parameters[1]);
            }
            else
            {
                updatedCount = catalog.Delete(string.Empty, command.Parameters[0]);
            }

            if (updatedCount > 0)
            {
                output.AppendLine(String.Format("{0} products deleted", updatedCount));
            }
            else
            {
                output.AppendLine("No products found");
            }
        }

        private static void FindProductsByNameCommand(Catalog catalog, Command command, StringBuilder output)
        {
            var contentList = catalog.GetContentByName(command.Parameters[0]);
            if (contentList != null && contentList.Count() > 0)
            {
                foreach (var content in contentList)
                {
                    output.AppendLine(content.ToString());
                }
            }
            else
            {
                output.AppendLine("No products found");
            }
        }

        private void FindProductsByPriceRangeCommand(Catalog catalog, Command command, StringBuilder output)
        {
            var contentList = catalog.GetContentByPriceRange(double.Parse(command.Parameters[0]), double.Parse(command.Parameters[1]));
            if (contentList != null && contentList.Count() > 0)
            {
                foreach (var content in contentList)
                {
                    output.AppendLine(content.ToString());
                }
            }
            else
            {
                output.AppendLine("No products found");
            }
        }

        private void FindProductsByProducerCommand(Catalog catalog, Command command, StringBuilder output)
        {
            var contentList = catalog.GetContentByProducer(command.Parameters[0]);
            if (contentList != null && contentList.Count() > 0)
            {
                foreach (var content in contentList)
                {
                    output.AppendLine(content.ToString());
                }
            }
            else
            {
                output.AppendLine("No products found");
            }
        }
    }
}