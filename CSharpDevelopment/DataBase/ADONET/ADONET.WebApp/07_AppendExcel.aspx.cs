using System;
using System.Data;
using System.Data.OleDb;

namespace ADONET.WebApp
{
    public partial class AppendExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            OleDbConnectionStringBuilder csbuilder = new OleDbConnectionStringBuilder();
            csbuilder.Provider = "Microsoft.ACE.OLEDB.12.0";
            csbuilder.DataSource = Server.MapPath(@"demo.xlsx");
            csbuilder.Add("Extended Properties", "Excel 12.0 Xml;HDR=YES");

            string queryText = @"INSERT INTO [Sheet1$] (Name, Score) VALUES (@Name, @Score)";

            using (OleDbConnection oConn = new OleDbConnection(csbuilder.ConnectionString))
            {
                using (OleDbCommand oRS = oConn.CreateCommand())
                {
                    oConn.Open();

                    oRS.CommandType = CommandType.Text;
                    oRS.CommandText = queryText;
                    oRS.Parameters.AddWithValue("@Name", "saykor");
                    oRS.Parameters.AddWithValue("@Score", "55");
                    oRS.ExecuteNonQuery();
                }
            }
        }
    }
}