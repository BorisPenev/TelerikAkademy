using System;
using System.Data.SqlClient;
using AdoNet.Data;

namespace ADONET.WebApp
{
    public partial class GetCategoryNameByProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(
                "Write a program that retrieves from the Northwind database all product categories and the names of the products in each category. Can you do this with a single SQL query (with table join)?<br>");

            string query = 
                "SELECT c.CategoryName, p.ProductName From Categories c JOIN Products p ON c.CategoryID = p.CategoryID";

            SqlProvider.ExecuteSqlQueryReturnValue(query, null, delegate(SqlDataReader reader)
                {
                    grdResult.DataSource = reader;
                    grdResult.DataBind();
                });
        }
    }
}