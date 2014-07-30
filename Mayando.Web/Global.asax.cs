using System.Globalization;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using EmbroideryFile;
using JelleDruyts.Web.Mvc;
using Mayando.Web.Controllers;
using Mayando.Web.Extensions;
using Mayando.Web.Infrastructure;
using Mayando.Web.Models;
using Mayando.Web.Repository;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using NinjectAdapter;

namespace Mayando.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        #region Constants

        internal const string RouteNameDefault = "Default";

        #endregion

        #region Register Routes

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

         
           


            routes.MapRoute(
                "embro/details/id/by/context/criteria",
                string.Format(CultureInfo.InvariantCulture, "{0}/{1}", EmbroController.ControllerName, ActionName.Details.ToActionString()) + "/{id}/by/{context}/{criteria}",
                new { controller = EmbroController.ControllerName, action = ActionName.Details.ToActionString(), criteria = UrlParameter.Optional }
            );


            routes.MapRoute(
              "embro/index/page/count/criteria",
              string.Format(CultureInfo.InvariantCulture, "{0}/{1}", EmbroController.ControllerName, ActionName.Index.ToActionString()) + "/{page}/{count}/{criteria}",
              new { controller = EmbroController.ControllerName, action = ActionName.Index.ToActionString(), page = 1, count = 5, criteria = UrlParameter.Optional }
          );

            routes.MapRoute(
                          "embro/index/id",
                          url: "embro/index/{id}",
                        defaults: new { controller = "embro", action = "index", page = 1, count = 7 }
                    );

            routes.MapRoute(
                     "contour/contoursvg/countryname/size",
                     "contour/contoursvg/{countryname}/{size}",
                     new { controller = ContourController.ControllerName, action = ActionName.ContourSvg.ToActionString(), countryName = "Russia", size = 400 }
                 );

            routes.MapRoute(
                          "embro/index/page/count",
                          url: "embro/index/{page}/{count}",
                        defaults: new { controller = "embro", action = "index", page = 1, count = 7 }
                    );

            routes.MapRoute(
     RouteNameDefault,
     url: "{controller}/{action}/{page}/{count}",
   defaults: new { controller = "embro", action = "index", page = 1, count = 7 });
            routes.MapRoute(
          "embro/detailsbyid",
           "embro/detailsbyid/{id}",
           new { controller = "embro", action = "detailsbyid", id = UrlParameter.Optional }
       );

            routes.MapRoute(
                "Root",
                "",
                new { controller = EmbroController.ControllerName, action = ActionName.Index.ToActionString() }
            );
        }

        #endregion

        #region Application Start & End

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        protected void Application_Start()
        {
            Logger.Log(LogLevel.Information, string.Format(CultureInfo.InvariantCulture, "Application started (v{0}).", Assembly.GetExecutingAssembly().GetName().Version.ToString()));
            // Create Ninject DI Kernel
            //IKernel kernel = new StandardKernel();

            // Register services with our Ninject DI Container
            //RegisterServices(kernel);

             //RegisterMyDependencyResolver();    
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            // Tell ASP.NET MVC 3 to use our Ninject DI Container
            //DependencyResolver.SetResolver(new NinjectServiceLocator(kernel));
           
            
            RegisterRoutes(RouteTable.Routes);

            // Replace the Default WebFormViewEngine with our custom ThemedWebFormViewEngine
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new WebFormViewEngine());
            ViewEngines.Engines.Add(new RazorViewEngine());              
            ViewEngines.Engines.Add(new ThemedWebFormViewEngine());
            
           
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        protected void Application_End()
        {
            Logger.Log(LogLevel.Information, string.Format(CultureInfo.InvariantCulture, "Application shut down for the following reason: {0}.", HostingEnvironment.ShutdownReason.ToString()));
        }

        #endregion
        #region [Ninject]
        public static void RegisterServices(IKernel kernel)
        {
            //kernel.Bind<IEmbroRepository>().To<EmbroRepository>().InSingletonScope();
            //kernel.Bind<ILinksRepository>().To<LinksRepository>().InSingletonScope();
            //kernel.Bind<ISvgEncode>().To<SvgEncoder>().InSingletonScope();
            //kernel.Bind<IContourRepository>().To<ContourRepository>().InSingletonScope();

        }



        private void RegisterMyDependencyResolver()
        {
            var standardKernel = new StandardKernel();
            RegisterServices(standardKernel);
            
            ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(standardKernel));
            DependencyResolver.SetResolver(new NinjectDependencyResolver());
        }
        #endregion [Ninject]
    }
}