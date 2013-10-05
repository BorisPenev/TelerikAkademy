using System;
using System.Data;
using System.Data.OleDb;

namespace ADONET.WebApp
{
    public partial class ReadFromExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet sheet1 = new DataSet();
            OleDbConnectionStringBuilder csbuilder = new OleDbConnectionStringBuilder();
            csbuilder.Provider = "Microsoft.ACE.OLEDB.12.0";
            csbuilder.DataSource = Server.MapPath(@"demo.xlsx");
            csbuilder.Add("Extended Properties", "Excel 12.0 Xml;HDR=YES");

            using (OleDbConnection connection = new OleDbConnection(csbuilder.ConnectionString))
            {
                connection.Open();
                string selectSql = @"SELECT * FROM [Sheet1$]";
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectSql, connection))
                {
                    adapter.Fill(sheet1);
                    grdResult.DataSource = sheet1;
                    grdResult.DataBind();
                }
                connection.Close();
            }
        }
    }
}