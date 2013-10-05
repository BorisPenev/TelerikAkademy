using System;
using System.Collections.Generic;
using System.Linq;

namespace BankSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Account> accounts = new List<Account>()
            {
                new DepositAccount(){ Balance = 999, Customer = new Customer(){ CustomerType = CustomerTypes.Company }, MonthlyInterestRate = 5 },
                new DepositAccount(){ Balance = 1200, Customer = new Customer(){ CustomerType = CustomerTypes.Individual }, MonthlyInterestRate = 5 },
                new LoanAccount(){ Balance = 999, Customer = new Customer(){ CustomerType = CustomerTypes.Company }, MonthlyInterestRate = 5 },
                new LoanAccount(){ Balance = 999, Customer = new Customer(){ CustomerType = CustomerTypes.Individual }, MonthlyInterestRate = 5 },
                new MortgageAccount(){ Balance = 999, Customer = new Customer(){ CustomerType = CustomerTypes.Company }, MonthlyInterestRate = 5 },
                new MortgageAccount(){ Balance = 999, Customer = new Customer(){ CustomerType = CustomerTypes.Individual }, MonthlyInterestRate = 5 },
            };
            accounts.ForEach(a =>
                {
                    Console.WriteLine(a.CalculateInterestRate(5));
                });
            Console.WriteLine("***************");
            accounts.ForEach(a =>
                {
                    Console.WriteLine(a.CalculateInterestRate(13));
                });
        }
    }
}
