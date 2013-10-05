using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Intro
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSum_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFirstNumber.Text) && !string.IsNullOrEmpty(txtSecondNumber.Text))
            {
                lblResult.Text = (int.Parse(txtFirstNumber.Text) + int.Parse(txtSecondNumber.Text)).ToString();
            }
        }
    }
}