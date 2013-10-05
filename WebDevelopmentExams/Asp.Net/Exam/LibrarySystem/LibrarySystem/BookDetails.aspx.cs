using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibrarySystem.Helpers;
using LibrarySystem.Models;

namespace LibrarySystem
{
    public partial class BookDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public Book SelectBook()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
            {
                var db = new ApplicationDbContext();
                return db.Books.Find(Request.QueryString["Id"].ToInt());
            }
            return null;
        }
    }
}