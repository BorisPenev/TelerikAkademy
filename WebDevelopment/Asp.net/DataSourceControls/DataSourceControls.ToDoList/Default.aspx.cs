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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTitle.Text) &&
                !string.IsNullOrEmpty(txtBody.Text) &&
                ddlCategories.SelectedIndex >= 0)
            {
                int categoryId = int.Parse(ddlCategories.SelectedValue);
                int todoId = 0;
                if (!string.IsNullOrEmpty(btnSave.CommandArgument))
                {
                    todoId = int.Parse(btnSave.CommandArgument);
                }

                var db = new TodosEntities();
                var todo = db.Todoes.Find(todoId);
                if (todo == null)
                {
                    todo = new Todo();
                }
                todo.Title = txtTitle.Text;
                todo.Body = txtBody.Text;
                todo.CategoryId = categoryId;
                todo.LastChangeDate = DateTime.Now;

                db.Todoes.AddOrUpdate(todo);
                db.SaveChanges();

                txtTitle.Text = string.Empty;
                txtBody.Text = string.Empty;
                btnSave.CommandArgument = string.Empty;

                grdTodos.DataBind();
            }
        }

        protected void grdTodos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            txtTitle.Text = grdTodos.Rows[e.NewEditIndex].Cells[1].Text;
            txtBody.Text = grdTodos.Rows[e.NewEditIndex].Cells[2].Text;
            btnSave.CommandArgument = grdTodos.DataKeys[e.NewEditIndex].Value.ToString();
            e.Cancel = true;
        }
    }
}