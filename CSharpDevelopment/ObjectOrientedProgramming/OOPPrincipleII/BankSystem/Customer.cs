using System;
using System.Linq;

namespace BankSystem
{
    //sled kato nikade v uslovieto ne e ukazano nqkakvo specialno razdelenie da polzvat 2ta vida customeri nqma zadaljenieto da se pravqt v otdelni clasove koyto da nasledqvat edin abstrakten.
    //polzvaneto na enumerator ne pravi po-malko OOP ili po-malko pravilno tova reshenie.
    public class Customer
    {
        private CustomerTypes customerType = CustomerTypes.Company;
        public CustomerTypes CustomerType
        {
            get { return customerType; }
            set { customerType = value; }
        }
        
    }
}
