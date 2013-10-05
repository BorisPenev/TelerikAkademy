using System;
using System.Transactions;
using EntityFramework.Data;

namespace EntityFramework.WebApp
{
    public partial class AddOrderWithItems : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("Create a method that places a new order in the Northwind database. The order should contain several order items. Use transaction to ensure the data consistency.<br>");

            using (TransactionScope scope = new TransactionScope())
            {
                SessionState.db.Orders.Add(new Order());
                SessionState.db.Orders.Add(new Order());
                SessionState.db.Orders.Add(new Order());
                SessionState.db.Orders.Add(new Order());
                SessionState.db.Orders.Add(new Order());
                SessionState.db.Orders.Add(new Order());

                SessionState.db.SaveChanges();
                scope.Complete();
            }
        }
    }
}