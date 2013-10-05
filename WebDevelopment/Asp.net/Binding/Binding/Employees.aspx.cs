using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Binding
{
    public partial class Employees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                NORTHWNDEntities db = new NORTHWNDEntities();
                grdEmployers.DataSource = db.Employees.Select(x => new
                {
                    Id = x.EmployeeID,
                    FullName = x.FirstName + " " + x.LastName
                }).ToList();
                grdEmployers.DataBind();

                rEmployers.DataSource = db.Employees.ToList();
                rEmployers.DataBind();

                lvEmps.DataSource = db.Employees.ToList();
                lvEmps.DataBind();
            }
        }

        protected void grdEmployers_SelectedIndexChanged(object sender, EventArgs e)
        {
            int eId = int.Parse(grdEmployers.SelectedDataKey.Value.ToString());
            NORTHWNDEntities db = new NORTHWNDEntities();
            grdFormView.DataSource = new List<Employee>()
                {
                    db.Employees.FirstOrDefault(x => x.EmployeeID == eId)
                };
            grdFormView.DataBind();
        }
    }
}