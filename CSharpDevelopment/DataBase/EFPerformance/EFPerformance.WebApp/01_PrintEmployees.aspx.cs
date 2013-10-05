using System;
using System.Linq;
using EFPerformance.Data;

namespace EFPerformance.WebApp
{
    public partial class PrintEmployees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionState.db.Employees.ToList().ForEach(employ =>
                {
                    Response.Write(employ.FirstName + " " + employ.LastName + ", " + employ.Department.Name + ", " +
                                   employ.Address.Town.Name + "<br />");
                });

            SessionState.db.Employees.Include("Department").Include("Address").Include("Address.Town").ToList().ForEach(employ =>
                {
                    Response.Write(employ.FirstName + " " + employ.LastName + ", " + employ.Department.Name + ", " +
                                   employ.Address.Town.Name + "<br />");
                });
        }
    }
}