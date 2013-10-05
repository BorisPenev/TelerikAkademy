using System;
using System.Collections.Generic;
using AdoNet.Data;

namespace ADONET.WebApp
{
    public partial class AddProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(
                "Write a method that adds a new product in the products table in the Northwind database. Use a parameterized SQL command.<br>");
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO Products";

            Dictionary<string, object> parametters = new Dictionary<string, object>();
            parametters.Add("ProductName", txtProductName.Text);
            parametters.Add("Discontinued", false);

            SqlProvider.ExecuteSqlQueryInsert(query, parametters, delegate(int id)
            {
                grdResult.DataSource = new List<int>(){ {id} };
                grdResult.DataBind();
            });
        }
    }
}