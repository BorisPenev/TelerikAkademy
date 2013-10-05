using System;
using System.Linq;

namespace BankSystem
{
    public class DepositAccount : Account, IWithdrawable
    {
        public override decimal CalculateInterestRate(int numberOfMonths)
        {
            if (this.Balance > 0 && this.Balance < 1000)
                return 0;
            else
                return base.CalculateInterestRate(numberOfMonths);
        }
    }
}
