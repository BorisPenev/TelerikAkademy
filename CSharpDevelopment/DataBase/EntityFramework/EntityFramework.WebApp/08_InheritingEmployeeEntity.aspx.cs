using System;
using System.Linq;
using EntityFramework.Data;

namespace EntityFramework.WebApp
{
    public partial class InheritingEmployeeEntity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("By inheriting the Employee entity class create a class which allows employees to access their corresponding territories as property of type EntitySet<T>.<br>");

            grdResult.DataSource = SessionState.db.Employees.FirstOrDefault().TerritoriesSet;
            grdResult.DataBind();


        }
    }
}