using System;
using System.Linq;
using EntityFramework.Data;

namespace EntityFramework.WebApp
{
    public partial class FindsAllCustomers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("Write a method that finds all customers who have orders made in 1997 and shipped to Canada<br>");

            var dbSearch =
                SessionState.db.Orders.Where(o => o.OrderDate.Value.Year == 1997 && o.ShipCountry == "Canada")
                  .Select(o => o.Customer)
                  .ToList();
            
            grdResult.DataSource = dbSearch;
            grdResult.DataBind();
        }
    }
}