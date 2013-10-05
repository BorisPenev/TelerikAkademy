using System;
using System.Linq;

namespace BankSystem
{
    public class MortgageAccount : Account
    {
        public override decimal CalculateInterestRate(int numberOfMonths)
        {
            if (this.Customer.CustomerType == CustomerTypes.Company)
            {
                if (numberOfMonths > 12)
                    numberOfMonths -= 6;
                else
                    numberOfMonths /= 2;
            }
            else
            {
                if (numberOfMonths > 6)
                    numberOfMonths -= 3;
                else
                    numberOfMonths /= 2;
            }

            if (numberOfMonths > 0)
                return base.CalculateInterestRate(numberOfMonths);
            else
                return 0;
        }
    }
}
