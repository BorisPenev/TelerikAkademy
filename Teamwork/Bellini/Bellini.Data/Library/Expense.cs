namespace Bellini.Data.Library
{
    public class Expense
    {
        public int Id { get; set; }
        public string Month { get; set; }
        public float Sum { get; set; }

        public Expense(string month, float sum)
        {
            this.Month = month;
            this.Sum = sum;
        }

        public int VendorExpenseId { get; set; }
        public VendorExpenseSql VendorExpense { get; set; }

    }
}
