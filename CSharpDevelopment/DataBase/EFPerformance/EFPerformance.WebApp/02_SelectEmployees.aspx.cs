using System;
using System.Linq;
using EFPerformance.Data;

namespace EFPerformance.WebApp
{
    public partial class SelectEmployees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var dbSearch = SessionState.db.Employees.ToList()
                                       .Select(a => a.Address)
                                       .ToList()
                                       .Select(t => t.Town)
                                       .ToList()
                                       .Where(t => t.Name == "Sofia").ToList();

            var dbSearch2 = SessionState.db.Employees.Where(em => em.Address.Town.Name == "Sofia").ToList();
        }
    }
}