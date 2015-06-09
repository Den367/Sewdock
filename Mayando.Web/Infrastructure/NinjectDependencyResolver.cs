using System;
using System.Collections.Generic;

using EmbroideryFile;
using Myembro.Models;
using Myembro.Repository;
using Ninject;
using Ninject.Syntax;

using IDependencyResolver = System.Web.Mvc.IDependencyResolver;


namespace Myembro.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        public IBindingToSyntax<T> Bind<T>()
        {
            return kernel.Bind<T>();
        }
        public IKernel Kernel
        {
            get { return kernel; }
        }
        private void AddBindings()
        {
            // put additional bindings here
            kernel.Bind<IAboutRepository>().To<AboutRepository>().InSingletonScope();
            kernel.Bind<IEmbroRepository>().To<EmbroRepository>().InSingletonScope();
            kernel.Bind<IThumbRepository>().To<ThumbRepository>().InSingletonScope();
           kernel.Bind<IGlyphRepository>().To<GlyphRepository>().InSingletonScope();
           kernel.Bind<ISvgEncode>().To<SvgEncoder>().InSingletonScope();
        }

      
    }
}