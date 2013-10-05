using System;
using System.Linq;

namespace MenuLinks
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MenuOption[] options = { new MenuOption("site1", "site1.aspx"), 
                                       new MenuOption("site2", "site2.aspx"),
                                       new MenuOption("site3", "site3.aspx"),
                                       new MenuOption("site4", "site4.aspx") };

            LinkMenu.FontFamily = "Segoe";
            LinkMenu.FontColor = "grey";
            LinkMenu.DataSource = options;
            LinkMenu.DataBind();
        }
    }
}