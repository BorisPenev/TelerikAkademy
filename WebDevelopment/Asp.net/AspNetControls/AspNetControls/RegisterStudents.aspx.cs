using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspNetControls
{
    public partial class RegisterStudents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var univestityDict = new Dictionary<int, string>();
                univestityDict.Add(1, "Rusenski Universitet");
                univestityDict.Add(2, "ПУТКА-Пернишки-университет-по-телекомуникации-компютри-и-архитектура");

                ddlUniversity.DataSource = univestityDict;
                ddlUniversity.DataValueField = "key";
                ddlUniversity.DataTextField = "value";
                ddlUniversity.DataBind();

                Dictionary<int, string> coursesDict = new Dictionary<int, string>();
                coursesDict.Add(1, "C#");
                coursesDict.Add(2, "javascript");

                lbCourse.DataSource = coursesDict;
                lbCourse.DataValueField = "key";
                lbCourse.DataTextField = "value";
                lbCourse.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ListItem item in lbCourse.Items)
            {
                if (item.Selected)
                {
                    sb.Append(item.Text + "<br>");
                }
            }
            Response.Write("<h1>Student: " + txtFirstName.Text + " " + txtLastName.Text + ", facility#: "+ txtFacilityNumber.Text +"</h1>"+
                            "<p> Univesity: "+ ddlUniversity.SelectedItem.Text +"<br/>"+ 
                            sb.ToString() +" </p>");
        }
    }
}