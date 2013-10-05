using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataSourceControls.WebData;

namespace DataSourceControls.ToDoList
{
    public partial class Categories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                int categoryId = 0;
                if (!string.IsNullOrEmpty(btnSave.CommandArgument))
                {
                    categoryId = int.Parse(btnSave.CommandArgument);
                }

                var db = new TodosEntities();
                var c = db.Categories.Find(categoryId);
                if (c == null)
                {
                    c = new Category();
                }
                c.Name = txtName.Text;
                db.Categories.AddOrUpdate(c);
                db.SaveChanges();
                txtName.Text = string.Empty;
                btnSave.CommandArgument = string.Empty;
                lbCategories.DataBind();
            }
        }

        protected void lbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtName.Text = lbCategories.SelectedItem.Text;
            btnSave.CommandArgument = lbCategories.SelectedItem.Value;
        }
    }
}