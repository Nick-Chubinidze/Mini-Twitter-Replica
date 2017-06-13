using Ninject;
using System;
using System.Web.Mvc;
using System.Web.Routing;
using Domain.Abstract;
using Domain.Entities;
using Domain.Concrete;
using Ninject.Web.Common;

namespace Twitter_Nika_Chubinidze.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IDbContextTwitter>().To<TwitterContext>().InSingletonScope();

            ninjectKernel.Bind<BaseRepository<Tweet>>().To<TweetRepository>();
            ninjectKernel.Bind<BaseRepository<TwitterUser>>().To<UserRepository>();
            ninjectKernel.Bind<BaseRepository<Photo>>().To<PhotoRepository>();
            ninjectKernel.Bind<BaseRepository<FollowerUser>>().To<FollowerUserRepository>();
        }
    }
}