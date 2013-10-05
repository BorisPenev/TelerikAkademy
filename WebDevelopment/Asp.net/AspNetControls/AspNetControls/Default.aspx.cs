using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspNetControls
{
    public partial class Default : System.Web.UI.Page
    {
        Random random = new Random();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGetRange_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFirstNumber.Text) && !string.IsNullOrEmpty(txtSecondNumber.Text))
            {
                lblRangeResult.Text =
                    random.Next(int.Parse(txtFirstNumber.Text), int.Parse(txtSecondNumber.Text)).ToString();

            }
        }
    }
}