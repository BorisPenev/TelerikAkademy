using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Error_Handler_Control;
using LibrarySystem.Helpers;
using LibrarySystem.Models;

namespace LibrarySystem.Admin
{
    public partial class Categories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && grdCategories.Rows.Count == 0)
            {
                CreatePanelVisibility(true);
            }
        }

        public IQueryable<Category> SelectCategories()
        {
            var db = new ApplicationDbContext();
            return db.Categories;
        }

        protected void btnCreateNew_Click(object sender, EventArgs e)
        {
            CreatePanelVisibility(true);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCategoryName.Text))
            {
                var categoryStatus = ActionType.Modified;

                var db = new ApplicationDbContext();
                var category = db.Categories.Find(btnSave.CommandArgument.ToInt());

                if (category == null && btnSave.CommandName != "Delete")
                {
                    category = new Category();
                    db.Categories.Add(category);
                    categoryStatus = ActionType.Created;
                }

                if (btnSave.CommandName == "Delete")
                {
                    db.Books.RemoveRange(category.Books);
                    db.Categories.Remove(category);
                    categoryStatus = ActionType.Deleted;
                }
                else
                {
                    category.Name = txtCategoryName.Text;
                }

                db.SaveChanges();
                grdCategories.DataBind();
                if (grdCategories.PageIndex == grdCategories.PageCount)
                {
                    grdCategories.PageIndex = 0;
                }

                ErrorSuccessNotifier.AddSuccessMessage("Category " + categoryStatus.ToString());
                CreatePanelVisibility(false);
            }
            else
            {
                ErrorSuccessNotifier.AddErrorMessage("Category Name is required!");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CreatePanelVisibility(false);
        }

        private void CreatePanelVisibility(bool isVisible)
        {
            btnSave.Text = "Create";
            btnSave.CommandArgument = string.Empty;
            btnSave.CommandName = string.Empty;
            btnCancel.Text = "Cancel";
            lblCategoryHeader.InnerText = "Create New Category";
            txtCategoryName.Text = string.Empty;
            txtCategoryName.Enabled = true;

            if (isVisible)
            {
                btnCreateNew.Visible = false;
                pnlCategory.Visible = true;
                
            }
            else
            {
                btnCreateNew.Visible = true;
                pnlCategory.Visible = false;
                
            }
        }

       
        protected void grdCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreatePanelVisibility(true);

            lblCategoryHeader.InnerText = "Confirm Category Deletion?";
            txtCategoryName.Text = Server.HtmlDecode(grdCategories.SelectedRow.Cells[0].Text);
            txtCategoryName.Enabled = false;
            btnSave.Text = "Yes";
            btnSave.CommandArgument = grdCategories.SelectedValue.ToString();
            btnSave.CommandName = "Delete";
            btnCancel.Text = "No";
        }

        protected void grdCategories_RowEditing(object sender, GridViewEditEventArgs e)
        {
            e.Cancel = true;
            CreatePanelVisibility(true);
            lblCategoryHeader.InnerText = "Edit Category";
            txtCategoryName.Text = Server.HtmlDecode(grdCategories.Rows[e.NewEditIndex].Cells[0].Text);
            btnSave.Text = "Save";
            btnSave.CommandArgument = grdCategories.DataKeys[e.NewEditIndex].Value.ToString();
        }
    }

    enum ActionType
    {
        Created,
        Modified,
        Deleted
    }
}