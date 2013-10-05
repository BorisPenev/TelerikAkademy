using System;
using System.Collections.Generic;
using AdoNet.Data;
using MySql.Data.MySqlClient;

namespace ADONET.WebApp
{
    public partial class MySQLBooks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grdResultFill();
            }
        }

        private void grdResultFill()
        {
            string query = "SELECT * FROM Books";
            MySqlProvider.ExecuteSqlQueryReturnValue(query, null, delegate(MySqlDataReader reader)
                {
                    grdResult.DataSource = reader;
                    grdResult.DataBind();
                });
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO Books";

            Dictionary<string, object> parametters = new Dictionary<string, object>();
            parametters.Add("Title", txtBookTitle.Text);
            parametters.Add("Author", txtBookAuthor.Text);
            parametters.Add("PublishDate", DateTime.Now);

            MySqlProvider.ExecuteSqlQueryInsert(query, parametters, delegate(int i)
                {
                    grdResultFill();
                });
        }

        protected void grdResult_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            string query = "DELETE FROM Books WHERE Id = @Id";
            
            Dictionary<string, object> parametters = new Dictionary<string, object>();
            parametters.Add("Id", e.Keys[0]);
            
            MySqlProvider.ExecuteSqlQueryReturnValue(query, parametters, result => grdResultFill() );
        }

        protected void grdResult_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            string query = "UPDATE Books";
            string where = "WHERE Id = " + e.Keys[0];

            Dictionary<string, object> parametters = new Dictionary<string, object>();
            parametters.Add("Title", e.NewValues[0] == null ? string.Empty : e.NewValues[0]);
            parametters.Add("Author", e.NewValues[1] == null ? string.Empty : e.NewValues[1]);

            MySqlProvider.ExecuteSqlQueryUpdate(query, where, parametters, reader => grdResult_RowCancelingEdit(sender, null) );
        }

        protected void grdResult_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            grdResult.EditIndex = e.NewEditIndex;
            grdResultFill();
        }

        protected void grdResult_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            grdResult.EditIndex = -1;
            grdResultFill();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM books WHERE LOCATE (@SearchOption, Title)";

            Dictionary<string, object> parametters = new Dictionary<string, object>();
            parametters.Add("SearchOption", txtSearch.Text);

            MySqlProvider.ExecuteSqlQueryReturnValue(query, parametters, delegate(MySqlDataReader reader)
            {
                grdResult.DataSource = reader;
                grdResult.DataBind();
            });
        }
    }
}