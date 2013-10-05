using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using ATM.Data;
using ATM.Data.Migrations;
using ATM.Models;

namespace ATM.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ATMContext, Configuration>());

            InitialDataImport();

            CashWithdraw("1234567894", "2234", 111.10M);
        }

        private static void InitialDataImport()
        {
            using (var db = new ATMContext())
            {
                var account1 = new CardAccount
                {
                    CardNumber = "1234567893",
                    CardPin = "1234",
                    CardCash = 5000.00m
                };
                var account2 = new CardAccount
                {
                    CardNumber = "1234567894",
                    CardPin = "2234",
                    CardCash = 5000.00m
                };
                var account3 = new CardAccount
                {
                    CardNumber = "1234567895",
                    CardPin = "3234",
                    CardCash = 5000.00m
                };

                db.CardAccounts.Add(account1);
                db.CardAccounts.Add(account2);
                db.CardAccounts.Add(account3);
                db.SaveChanges();
                db.Database.ExecuteSqlCommand("CREATE UNIQUE INDEX IX_CardNumber ON CardAccounts (CardNumber)");
            }
        }

        public static void CashWithdraw(string cardNumber,
            string cardPin, decimal sum)
        {
            using (var db = new ATMContext())
            {
                bool success = false;
                using (TransactionScope transaction = new TransactionScope())
                {
                    try
                    {
                        // get card number and pin
                        var card = db.CardAccounts.
                            Where(c => c.CardNumber == cardNumber).
                            Where(p => p.CardPin == cardPin).First();

                        if (card.CardCash >= sum)
                        {
                            // withdraw sum
                            card.CardCash -= sum;
                        }
                        else
                        {
                            Console.WriteLine("Insufficient funds!");
                        }

                        transaction.Complete();
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Wrong PIN or Invalid card!");
                    }
                }
                if (success)
                {
                    // save in history
                    var log = new TransactionsHistory()
                    {
                        CardNumber = cardNumber,
                        TransactionDate = DateTime.Now,
                        Ammount = sum
                    };

                    db.TransactionsHisory.Add(log);

                    db.SaveChanges();
                    Console.WriteLine("Transaction successful!");
                }
            }
        }
    }
}
