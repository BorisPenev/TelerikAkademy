using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WcfDemo.Client.WebService;

namespace WcfDemo.Client
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var client = new DemoServiceClient();
            Response.Write(client.GetDayOfWeek(DateTime.Now));

            Response.Write(client.GetStringRepeatedCount("Create a Web service library which accepts two string as parameters. It should return the number of times the second string contains the first string. Test it with the integrated WCF client.", "the"));
        }
    }
}