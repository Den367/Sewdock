using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using Ninject;

namespace Mayando.Web
{
    public class MyDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public MyDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return ServiceLocator.Current.GetInstance(serviceType);
            }
            catch (Microsoft.Practices.ServiceLocation.ActivationException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return ServiceLocator.Current.GetAllInstances(serviceType);
            }
            catch (Microsoft.Practices.ServiceLocation.ActivationException)
            {
                return null;
            }
        }
    }
}