using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebSiteMusicBand.Model;
using Ninject;
using Ninject.Web.Common;
using WebSiteMusicBand.Controllers;

namespace WebSiteMusicBand.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
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

        private void AddBindings()
        {
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<INewsRepository>().To<NewsRepository>().WhenInjectedExactlyInto<NewsController>().WithConstructorArgument("section", "News");
            kernel.Bind<INewsRepository>().To<NewsRepository>().WhenInjectedExactlyInto<MembersController>().WithConstructorArgument("section", "Members"); 
            kernel.Bind<INewsRepository>().To<NewsRepository>().WhenInjectedExactlyInto<HistoryController>().WithConstructorArgument("section", "History");
            kernel.Bind<IAlbumRepository>().To<AlbumsRepository>();


            MvcApplication.logger.Trace("Bind njects");
        }
    }
}