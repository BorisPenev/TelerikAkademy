using System;
using System.Data.Entity;
using Bellini.Data;

namespace Bellini.WebApp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<SupermarketSQLDbContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SupermarketSQLDbContext, Data.Migrations.Configuration>());
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}