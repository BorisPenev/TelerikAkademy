using System;
using System.Linq;
using System.Transactions;
using EntityFramework.Data;

namespace EntityFramework.WebApp
{
    public partial class UsersInGroups : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("Create a database holding users and groups. Create a transactional EF based method that creates an user and puts it in a group \"Admins\". In case the group \"Admins\" do not exist, create the group in the same transaction. If some of the operations fail (e.g. the username already exist), cancel the entire transaction.<br>");
        }
        
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserName.Text))
            {
                User user = new User() {UserName = txtUserName.Text};
                AddUser(user);
            }
        }

        private static void AddUser(User user)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                SessionState.db.Users.Add(user);

                Group adminGroup = SessionState.db.Groups.FirstOrDefault(g => g.GroupName == "Admin");
                if (adminGroup == null)
                {
                    adminGroup = new Group() {GroupName = "Admin"};
                    SessionState.db.Groups.Add(adminGroup);
                }

                SessionState.db.SaveChanges();
                scope.Complete();
            }
        }

    }
}