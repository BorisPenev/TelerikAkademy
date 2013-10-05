using System;
using System.Linq;
using EntityFramework.Data;

namespace EntityFramework.Library
{
    class Dao
    {
        public static void Insert(string id, string name)
        {
            Customer newCustomer = new Customer()
                {
                    CompanyName = name,
                    CustomerID = id
                };

            bool isInDb = IsInDataBase(id);

            if (!isInDb)
            {
                SessionState.db.Customers.Add(newCustomer);
                SessionState.db.SaveChanges();
                Console.WriteLine("Added Successful.");
            }
            else
            {
                throw new ArgumentException("Such customer already exists");
            }
        }

        public static void Modify(string id, string newContactName)
        {
            var customer = SessionState.db.Customers.Where(x => x.CustomerID == id).FirstOrDefault();
            if (customer != null)
            {
                customer.ContactName = newContactName;
            }
            SessionState.db.SaveChanges();
        }

        public static void Delete(string id)
        {
            var customer = SessionState.db.Customers.Where(x => x.CustomerID == id).FirstOrDefault();
            if (customer != null)
            {
                SessionState.db.Customers.Remove(customer);
                SessionState.db.SaveChanges();
            }
        }

        private static bool IsInDataBase(string id)
        {
            bool alreadyInDB = SessionState.db.Customers.Where(a => a.CustomerID == id).Any();
            return alreadyInDB;
        }
    }
}