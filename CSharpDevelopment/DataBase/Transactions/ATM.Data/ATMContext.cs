using System;
using System.Data.Entity;
using System.Linq;
using ATM.Models;

namespace ATM.Data
{
    public class ATMContext : DbContext
    {
        public ATMContext()
            : base("ATMdb")
        {
        }

        public DbSet<CardAccount> CardAccounts { get; set; }

        public DbSet<TransactionsHistory> TransactionsHisory { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // cardAccount
            modelBuilder.Entity<CardAccount>().HasKey(x => x.Id);
            modelBuilder.Entity<CardAccount>().Property(x => x.CardNumber).
                IsUnicode(true).IsFixedLength().HasMaxLength(10);
            modelBuilder.Entity<CardAccount>().Property(x => x.CardPin).
                HasMaxLength(4).IsFixedLength();
            modelBuilder.Entity<CardAccount>().Property(x => x.CardCash).
                HasPrecision(10, 2);

            //transactionHystory
            modelBuilder.Entity<TransactionsHistory>().HasKey(x => x.Id);
            modelBuilder.Entity<TransactionsHistory>().Property(x => x.CardNumber).
                IsUnicode(true).IsFixedLength().HasMaxLength(10);
            modelBuilder.Entity<TransactionsHistory>().Property(x => x.Ammount).
                HasPrecision(10, 2);

            //// modelBuilder.Configurations.Add(new TagMappings());

            base.OnModelCreating(modelBuilder);
        }
    }
}
