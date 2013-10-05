using System;

namespace AspNetControls
{
    public partial class Escaping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSend.Text))
            {
                txtRead.Text = Server.HtmlEncode(txtSend.Text);
                lblRead.Text = Server.HtmlEncode(txtSend.Text);
            }
        }
    }
}