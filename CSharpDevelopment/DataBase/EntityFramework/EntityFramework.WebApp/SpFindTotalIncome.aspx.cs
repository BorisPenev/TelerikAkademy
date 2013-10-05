using System;
using System.Data.SqlClient;
using EntityFramework.Data;

namespace EntityFramework.WebApp
{
    public partial class SpFindTotalIncome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetIncome("Pesho", new DateTime(2012, 12, 01), new DateTime(2012, 12, 12));
        }

        private void GetIncome(string name, DateTime start, DateTime end)
        {
            SqlParameter param1 = new SqlParameter("@companyName", name);
            SqlParameter param2 = new SqlParameter("@start", start);
            SqlParameter param3 = new SqlParameter("@end", end);
            var result = SessionState.db.Database.ExecuteSqlCommand(
                "usp_FindTotalIncome @companyName, @start, @end", param1, param2, param3);

            Response.Write("Total Incomes from '" + name + "' are: " + result);
        }
    }
}