using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AdoNet.Data;

namespace ADONET.WebApp
{
    public partial class FindAllProducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(
                "Write a method that adds a new product in the products table in the Northwind database. Use a parameterized SQL command.<br>");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string query = "SELECT ProductName FROM Products WHERE CHARINDEX (@SearchOption, ProductName)>0";

            Dictionary<string, object> parametters = new Dictionary<string, object>();
            parametters.Add("SearchOption", txtSearchOption.Text);

            SqlProvider.ExecuteSqlQueryReturnValue(query, parametters, delegate(SqlDataReader reader)
                {
                    grdResult.DataSource = reader;
                    grdResult.DataBind();
                });
        }
    }
}