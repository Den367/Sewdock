using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EmbroideryFile;
using Mayando.Web.Models;
using Mayando.Web.Repository;
using Ninject;

namespace Mayando.Web.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext requestContext,
        Type controllerType)
        {
            return controllerType == null
            ? null
            : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            ninjectKernel.Bind<IAboutRepository>().To<AboutRepository>().InSingletonScope();
            ninjectKernel.Bind<ILinksRepository>().To<LinksRepository>().InSingletonScope();
            ninjectKernel.Bind<IEmbroRepository>().To<EmbroRepository>().InSingletonScope();
            ninjectKernel.Bind<IThumbRepository>().To<ThumbRepository>().InSingletonScope();
            ninjectKernel.Bind<ICommentRepository>().To<CommentRepository>().InSingletonScope();
            ninjectKernel.Bind<ISvgEncode>().To<SvgEncoder>().InSingletonScope();
            ninjectKernel.Bind<IContourRepository>().To<ContourRepository>().InSingletonScope();
           
       
        }
    }
}
