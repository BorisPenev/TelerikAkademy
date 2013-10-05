using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Binding
{
    public partial class EmpDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int eId = int.Parse(Request.QueryString["id"]);
                NORTHWNDEntities db = new NORTHWNDEntities();
                grdEmployerView.DataSource = new List<Employee>()
                {
                    db.Employees.FirstOrDefault(x => x.EmployeeID == eId)
                };
                grdEmployerView.DataBind();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Employees.aspx");
        }
    }
}