using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using Microsoft.Practices.ServiceLocation;

namespace MvcApplication1
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
            catch (Microsoft.Practices.ServiceLocation.ActivationException ex)
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
            catch (Microsoft.Practices.ServiceLocation.ActivationException ex)
            {
                return null;
            }
        }
    }
}
