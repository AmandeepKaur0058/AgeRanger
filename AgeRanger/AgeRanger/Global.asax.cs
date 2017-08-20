using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AgeRanger.Data;
using AgeRanger.Models;
using AgeRanger.Repositories;

namespace AgeRanger
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new ServiceContainer();
            container.RegisterControllers();
            //register other services

            container.EnableMvc();
            container.Register<IApplicationDbContext, ApplicationDbContext>(new PerScopeLifetime());            
            container.Register<IPersonRepository, PersonRepository>(new PerScopeLifetime());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
