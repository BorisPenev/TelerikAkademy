using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using Musicstore.Server.Data;
using Musicstore.Server.Data.Interfaces;
using Musicstore.Server.Data.Repositories;
using Musicstore.Server.Models;

namespace Musicstore.Server.WebApi
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer<MusicstoreContext>(null);
            Database.SetInitializer(new CreateDatabaseIfNotExists<MusicstoreContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MusicstoreContext,
                Data.Migrations.Configuration>());

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            var builder = new ContainerBuilder();

            #region data layer
            //var dataSettingsManager = new DataSettingsManager();
            //var dataProviderSettings = dataSettingsManager.LoadSettings();
            #endregion

            var db = new MusicstoreContext();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterGeneric(typeof(EfRepository<>)).AsImplementedInterfaces();
            builder.Register<IRepository>(c => new EfRepository(db)).InstancePerApiRequest();
            //builder.Register<IRepository<Artist>>(c => new EfRepository<Artist>(new MusicstoreContext())).InstancePerApiRequest();
            //builder.Register<IRepository<Song>>(c => new EfRepository<Song>(new MusicstoreContext())).InstancePerApiRequest();

            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}