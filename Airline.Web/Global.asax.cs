using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Airline.Web.Models;
using System.Data.Entity;
using Ninject.Modules;
using Airline.BLL.Ninject;
using Ninject;
//using Ninject.Web.Mvc;

namespace Airline.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new IdentityInitializer());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectModule registrations = new NinjectRegistrations();
            var kernel = new StandardKernel(registrations);
            DependencyResolver.SetResolver(new AirlineDependencyResolver(kernel));
            //DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
    public class AirlineDependencyResolver : IDependencyResolver
    {
        IKernel kernel;
        public AirlineDependencyResolver(IKernel _kernel)
        {
            kernel = _kernel;
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType, new Ninject.Parameters.IParameter[0]);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType, new Ninject.Parameters.IParameter[0]);
        }
    }
}
