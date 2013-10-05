using System.Collections.Generic;
using System.Xml;
using Bellini.Data.Library;
using Bellini.Library;

namespace Bellini.Data.Readers
{
    public class VendorExpenseReader
    {
        public static List<VendorExpenseSql> Read(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNodeList sales = doc.SelectNodes("//sale");
            List<VendorExpenseSql> vendorExpenses = new List<VendorExpenseSql>();
            foreach (XmlNode sale in sales)
            {
                string vendorName = sale.Attributes["vendor"].Value;
                VendorExpenseSql newVendorExpense = new VendorExpenseSql() { VendorName = vendorName };
                vendorExpenses.Add(newVendorExpense);

                List<Expense> expenses = new List<Expense>();
                foreach (XmlNode expense in sale.ChildNodes)
                {
                    string month = expense.Attributes["month"].Value;
                    string sum = expense.InnerText;
                    expenses.Add(new Expense(month, float.Parse(sum)));
                }
                newVendorExpense.Expenses = expenses;
            }

            return vendorExpenses;
        }
    }
}
