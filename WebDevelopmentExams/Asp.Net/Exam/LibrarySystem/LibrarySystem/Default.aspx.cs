using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Error_Handler_Control;
using LibrarySystem.Models;

namespace LibrarySystem
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<Category> SelectCategories()
        {
            var db = new ApplicationDbContext();
            return db.Categories.Include("Books");
        }

        protected void lvCategory_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var repeaterBooks = e.Item.FindControl("repeaterBooks") as Repeater;
                if (repeaterBooks != null)
                {
                    var category = e.Item.DataItem as Category;
                    if (category != null && category.Books.Count > 0)
                    {
                        repeaterBooks.DataSource = category.Books;
                        repeaterBooks.DataBind();
                    }
                    else
                    {
                        var lblNoRecords = e.Item.FindControl("lblNoRecords") as Label;
                        if (lblNoRecords != null)
                        {
                            lblNoRecords.Visible = true;
                        }
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length > 256)
            {
                ErrorSuccessNotifier.AddErrorMessage("Search criteria cannot me more than 256 symbols");
                return;
            }
            Response.Redirect("~/Search?q=" + txtSearch.Text);
        }
    }
}