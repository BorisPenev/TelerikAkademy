using System;
using System.Data.SqlClient;
using AdoNet.Data;

namespace ADONET.WebApp
{
    public partial class GetNumberOfRowsForCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("Write a program that retrieves from the Northwind sample database in MS SQL Server the number of  rows in the Categories table.<br>");

            string query = "SELECT COUNT(*) from [dbo].[Categories]";
            SqlProvider.ExecuteSqlQueryReturnValue(query, null, delegate(SqlDataReader reader)
                {
                    grdResult.DataSource = reader;
                    grdResult.DataBind();
                });
        }
    }
}