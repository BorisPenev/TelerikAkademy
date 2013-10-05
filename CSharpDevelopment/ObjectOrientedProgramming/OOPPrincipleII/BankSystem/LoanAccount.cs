using System;
using System.Linq;

namespace BankSystem
{
    public class LoanAccount : Account
    {
        public override decimal CalculateInterestRate(int numberOfMonths)
        {
            if (this.Customer.CustomerType == CustomerTypes.Individual)
                numberOfMonths -= 3;
            else
                numberOfMonths -= 2;

            if (numberOfMonths > 0)
                return base.CalculateInterestRate(numberOfMonths);
            else
                return 0;
        }
    }
}
