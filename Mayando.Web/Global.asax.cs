﻿using System.Globalization;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CaptchaMvc.Infrastructure;

using Myembro.App_Start;

using Myembro.Infrastructure;

using Microsoft.Practices.ServiceLocation;
using Ninject;
using NinjectAdapter;

namespace Myembro
{
    public class MvcApplication : System.Web.HttpApplication
    {


        #region Application Start & End

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        protected void Application_Start()
        {
            //CaptchaUtils.CaptchaManager.StorageProvider = new SessionStorageProvider();
            CaptchaUtils.CaptchaManager.StorageProvider = new CookieStorageProvider();
            Logger.Log(LogLevel.Information, string.Format(CultureInfo.InvariantCulture, "Application started (v{0}).", Assembly.GetExecutingAssembly().GetName().Version.ToString()));
            // Create Ninject DI Kernel
            //IKernel kernel = new StandardKernel();

            // Register services with our Ninject DI Container
            //RegisterServices(kernel);

             //RegisterMyDependencyResolver();    
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());



            RouteConfig.RegisterRoutes(RouteTable.Routes);
            // Manually installed WebAPI 2.2 after making an MVC project.
            GlobalConfiguration.Configure(WebApiConfig.Register); // NEW way
            //WebApiConfig.Register(GlobalConfiguration.Configuration); // DEPRECATED
            // Replace the Default WebFormViewEngine with our custom ThemedWebFormViewEngine
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new WebFormViewEngine());
            ViewEngines.Engines.Add(new RazorViewEngine());              
            //ViewEngines.Engines.Add(new ThemedWebFormViewEngine());
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           
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