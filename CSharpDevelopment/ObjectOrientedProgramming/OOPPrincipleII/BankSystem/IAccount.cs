using System;
using System.Linq;

namespace BankSystem
{
    public interface IAccount
    {
        Customer Customer
        {
            get;
            set;
        }

        decimal Balance
        {
            get;
            set;
        }

        decimal MonthlyInterestRate
        {
            get;
            set;
        }

        decimal CalculateInterestRate(int numberOfMonths);
    }
}
