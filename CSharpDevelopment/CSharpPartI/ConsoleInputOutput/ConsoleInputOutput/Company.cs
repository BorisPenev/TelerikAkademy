using System;
using System.Linq;

namespace ConsoleInputOutput
{
    public class Company
    {
        public string Name { set; get; }
        public string Address { set; get; }
        public string PhoneNumber { set; get; }
        public string FaxNumber { set; get; }
        public string WebSite { set; get; }
        public Manager CompanyManager = new Manager();
    }
}
