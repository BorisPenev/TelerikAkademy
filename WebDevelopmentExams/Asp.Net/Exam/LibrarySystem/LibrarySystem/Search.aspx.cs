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
    public partial class Search : System.Web.UI.Page
    {
        public string Query { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<Book> SelectBook()
        {
            var db = new ApplicationDbContext();
            if (!string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                Query = Request.QueryString["q"];
                return
                    db.Books.Include("Category")
                        .Where(
                            x =>
                                x.Title.ToLower().Contains(Query.ToLower()) ||
                                x.Author.ToLower().Contains(Query.ToLower()));

            }
            return db.Books.Include("Category");
        }
    }
}