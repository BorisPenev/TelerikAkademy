using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ValidateRegisterForm
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {

        }

        public void checkCheckBox(object o, ServerValidateEventArgs e)
        {
            e.IsValid = chkIAccept.Checked;
        }
    }
}