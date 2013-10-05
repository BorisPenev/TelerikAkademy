using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityFramework.Data;

namespace EntityFramework.WebApp
{
    public partial class CreateNorthwindTwin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("Create a database called NorthwindTwin with the same structure as Northwind using the features from DbContext. Find for the API for schema generation in MSDN or in Google.<br>");

            CreateDB();
            GetSchema();
        }

        private static void CreateDB()
        {
            using (var db = new NorthwindTwin())
            {
                SessionState.db.Database.CreateIfNotExists();
            }
        }

        private static void GetSchema()
        {
            ObjectContext objectContext = ((IObjectContextAdapter)SessionState.db).ObjectContext;
            var dbSchema = objectContext.CreateDatabaseScript();

            NorthwindTwin dbTwin = new NorthwindTwin();
            ObjectContext objectContext2 = ((IObjectContextAdapter)dbTwin).ObjectContext;
            objectContext2.ExecuteStoreCommand(dbSchema);
        }
    }

    internal class NorthwindTwin : DbContext
    {
    }
}