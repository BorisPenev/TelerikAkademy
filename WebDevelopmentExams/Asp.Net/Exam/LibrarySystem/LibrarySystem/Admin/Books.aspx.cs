using System;
using System.Collections;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;
using Error_Handler_Control;
using LibrarySystem.Helpers;
using LibrarySystem.Models;

namespace LibrarySystem.Admin
{
    public partial class Books : System.Web.UI.Page
    {
        private const int MaxAllowedTextLength = 20;
        private bool isValidationError = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (grdBooks.Rows.Count == 0)
            {
                CreatePanelVisibility(true);
            }
        }

        public IEnumerable SelectCategories()
        {
            var db = new ApplicationDbContext();
            return db.Categories.ToList();
        }

        public IQueryable<Book> SelectBooks([ViewState("OrderBy")]String orderBy = null)
        {
            var db = new ApplicationDbContext();
            var books = db.Books.Include("Category").AsQueryable();
            if (orderBy != null)
            {
                switch (this.SortDirection)
                {
                    case SortDirection.Ascending:
                        books = books.OrderByDescending(orderBy);
                        break;
                    case SortDirection.Descending:
                        books = books.OrderBy(orderBy);
                        break;
                    default:
                        books = books.OrderByDescending(orderBy);
                        break;
                }
            }
            return books;
        }

        protected void btnCreateNew_Click(object sender, EventArgs e)
        {
            CreatePanelVisibility(true);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.CommandName != "Delete")
            {
                if (string.IsNullOrEmpty(txtTitle.Text))
                {
                    isValidationError = true;
                    ErrorSuccessNotifier.AddErrorMessage("Title is required!");
                }

                if (txtTitle.Text.Length > 256)
                {
                    isValidationError = true;
                    ErrorSuccessNotifier.AddErrorMessage("Title field cannot be more than 256 length");
                }

                if (string.IsNullOrEmpty(txtAuthor.Text))
                {
                    isValidationError = true;
                    ErrorSuccessNotifier.AddErrorMessage("Author is required!");
                }

                if (txtAuthor.Text.Length > 256)
                {
                    isValidationError = true;
                    ErrorSuccessNotifier.AddErrorMessage("Author field cannot be more than 256 length");
                }

                if (txtIsbn.Text.Length > 256)
                {
                    isValidationError = true;
                    ErrorSuccessNotifier.AddErrorMessage("ISBN field cannot be more than 256 length");
                }

                if (txtWebSite.Text.Length > 256)
                {
                    isValidationError = true;
                    ErrorSuccessNotifier.AddErrorMessage("Website field cannot be more than 256 length");
                }
            }


            if (!isValidationError)
            {
                var categoryStatus = ActionType.Modified;

                var db = new ApplicationDbContext();
                var book = db.Books.Find(btnSave.CommandArgument.ToInt());

                if (book == null && btnSave.CommandName != "Delete")
                {
                    book = new Book();
                    db.Books.Add(book);
                    categoryStatus = ActionType.Created;
                }

                if (btnSave.CommandName == "Delete")
                {
                    db.Books.Remove(book);
                    categoryStatus = ActionType.Deleted;
                }
                else
                {
                    var website = txtWebSite.Text;
                    if (!website.StartsWith("http://") && !website.StartsWith("https://"))
                    {
                        website = "http://" + website;
                    }

                    book.Title = txtTitle.Text;
                    book.Author = txtAuthor.Text;
                    book.Isbn = txtIsbn.Text;
                    book.WebSite = website;
                    book.Description = txtDescription.Text;
                    book.CategoryId = ddlCategory.SelectedValue.ToInt();
                }

                db.SaveChanges();
                grdBooks.DataBind();
                ErrorSuccessNotifier.AddSuccessMessage("Book " + categoryStatus);
                CreatePanelVisibility(false);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CreatePanelVisibility(false);
        }

        protected void grdBooks_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            e.Cancel = true;
            CreatePanelVisibility(true);
            lblHeader.InnerText = "Edit Book";

            var db = new ApplicationDbContext();
            var book = db.Books.Find(grdBooks.DataKeys[e.NewEditIndex].Value.ToString().ToInt());
            if (book != null)
            {
                txtTitle.Text = book.Title;
                txtAuthor.Text = book.Author;
                txtIsbn.Text = book.Isbn;
                txtWebSite.Text = book.WebSite;
                txtDescription.Text = book.Description;
                ddlCategory.SelectedValue = book.CategoryId.ToString();
                btnSave.Text = "Save";
                btnSave.CommandArgument = book.Id.ToString();
                
            }
        }

        protected void grdBooks_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CreatePanelVisibility(true);

            lblHeader.InnerText = "Confirm Book Deletion?";
            txtTitle.Text = Server.HtmlDecode(grdBooks.SelectedRow.Cells[0].Text);
            txtTitle.Enabled = false;
            btnSave.Text = "Yes";
            btnSave.CommandArgument = grdBooks.SelectedValue.ToString();
            btnSave.CommandName = "Delete";
            btnCancel.Text = "No";
            pnlAdditionalInfo.Visible = false;
        }

        

        private void CreatePanelVisibility(bool isVisible)
        {
            btnSave.CommandArgument = string.Empty;
            btnSave.CommandName = string.Empty;
            lblHeader.InnerText = "Create New Book";
            txtTitle.Enabled = true;
            txtTitle.Text = string.Empty;
            txtAuthor.Text = string.Empty;
            txtIsbn.Text = string.Empty;
            txtWebSite.Text = string.Empty;
            txtDescription.Text = string.Empty;
            ddlCategory.SelectedIndex = -1;
            pnlAdditionalInfo.Visible = true;
            btnCancel.Text = "Cancel";
            btnSave.Text = "Create";

            if (isVisible)
            {
                btnCreateNew.Visible = false;
                pnlBooks.Visible = true;
            }
            else
            {
                btnCreateNew.Visible = true;
                pnlBooks.Visible = false;
            }
        }

        protected void grdBooks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var book = e.Row.DataItem as Book;
                if (book != null)
                {
                    e.Row.Cells[0].Text = Server.HtmlEncode(FormatLength(book.Title));
                    e.Row.Cells[1].Text = Server.HtmlEncode(FormatLength(book.Author));
                    var link = e.Row.Cells[3].Controls[0] as HyperLink;
                    if (link != null)
                    {
                        link.Text = Server.HtmlEncode(FormatLength(book.WebSite));
                        link.NavigateUrl = Server.HtmlEncode(book.WebSite);
                    }
                    e.Row.Cells[4].Text = Server.HtmlEncode(FormatLength(book.Category.Name));
                }
            }
        }

        private string FormatLength(string source)
        {
            if (source.Length > MaxAllowedTextLength)
            {
                return source.Substring(0, MaxAllowedTextLength - 3) + "...";
            }
            else
            {
                return source;
            }
        }

        protected void grdBooks_Sorting(object sender, GridViewSortEventArgs e)
        {
                e.Cancel = true;
                ViewState["OrderBy"] = e.SortExpression;
                grdBooks.DataBind();
        }

        public SortDirection SortDirection
        {
            get
            {
                if (ViewState["sortdirection"] == null)
                {
                    ViewState["sortdirection"] = SortDirection.Ascending;
                    return SortDirection.Ascending;
                }
                else if ((SortDirection)ViewState["sortdirection"] == SortDirection.Ascending)
                {
                    ViewState["sortdirection"] = SortDirection.Descending;
                    return SortDirection.Descending;
                }
                else
                {
                    ViewState["sortdirection"] = SortDirection.Ascending;
                    return SortDirection.Ascending;
                }
            }
            set
            {
                ViewState["sortdirection"] = value;
            }
        }
    }
}