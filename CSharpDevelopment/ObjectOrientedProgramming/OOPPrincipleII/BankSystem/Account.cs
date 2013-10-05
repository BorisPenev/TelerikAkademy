using System;
using System.Linq;

namespace BankSystem
{
    public abstract class Account : IDepositable, IAccount
    {
        private Customer customer = new Customer();
        public Customer Customer
        {
            get { return customer; }
            set { customer = value; }
        }
        

        public decimal Balance
        {
            get;
            set;
        }

        public decimal MonthlyInterestRate
        {
            get;
            set;
        }

        public virtual void Deposit()
        {
            throw new NotImplementedException();
        }

        public virtual decimal CalculateInterestRate(int numberOfMonths)
        {
            return numberOfMonths * this.MonthlyInterestRate;
        }
    }
}
