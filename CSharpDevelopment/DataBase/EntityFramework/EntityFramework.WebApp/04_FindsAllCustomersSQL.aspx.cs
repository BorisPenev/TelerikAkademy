using System;
using System.Collections.Generic;
using EntityFramework.Data;

namespace EntityFramework.WebApp
{
    public partial class FindsAllCustomersSQL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("Implement previous by using native SQL query and executing it through the DbContext<br>");

            string query = "SELECT DISTINCT c.* FROM Customers c, Orders o Where o.CustomerID = c.CustomerID AND YEAR(o.OrderDate) = 1997 AND o.ShipCountry = 'Canada'";
            var dbSearch = SessionState.db.Database.SqlQuery<Customer>(query);
           
            List<Customer> customers = new List<Customer>();
            customers.AddRange(dbSearch);

            grdResult.DataSource = customers;
            grdResult.DataBind();
        }
    }
}